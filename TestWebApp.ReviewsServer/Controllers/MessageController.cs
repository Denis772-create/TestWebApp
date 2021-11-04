using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using TestWebApp.DAL.Data;
using TestWebApp.DAL.DTO;
using TestWebApp.DAL.Models.ReviewModels;
using TestWebApp.ReviewServer.Hubs;
using TestWebApp.ReviewServer.Signatures;

namespace TestWebApp.ReviewServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHubContext<ReviewHub, IReviewHub> _hubContext;

        public MessageController(ApplicationDbContext context,
            IMapper mapper,
            IHubContext<ReviewHub, IReviewHub> hubContext)
        {
            _context = context;
            _mapper = mapper;
            _hubContext = hubContext;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RoomDto>> Get(int id)
        {
            var message = await _context.Messages.FindAsync(id);
            if (message == null)
                return BadRequest();

            var messageDto = _mapper.Map<Message, MessageDto>(message);
            return Ok(messageDto);
        }

        [HttpGet("room/{room-name}")]
        public IActionResult GetMessages(string roomName)
        {
            var room = _context.Rooms.FirstOrDefault(r => r.Name == roomName);
            if (room == null)
                return BadRequest();

            var messages = _context.Messages.Where(m => m.ToRoomId == room.Id)
                .Include(m => m.FromUser)
                .Include(m => m.ToRoom)
                .OrderByDescending(m => m.Timestamp)
                .Take(20)
                .AsEnumerable()
                .Reverse()
                .ToList();

            var messagesDto = _mapper.Map<IEnumerable<Message>, IEnumerable<MessageDto>>(messages);

            return Ok(messagesDto);
        }

        [HttpPost]
        public async Task<ActionResult<MessageDto>> Create(MessageDto messageDto)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
            var room = await _context.Rooms.FirstOrDefaultAsync(r => r.Name == messageDto.Room);
            if (room == null)
                return BadRequest();

            var msg = new Message
            {
                Content = Regex.Replace(messageDto.Content, "<.*?>", string.Empty),
                FromUser = user,
                ToRoom = room,
                Timestamp = DateTime.Now
            };

            _context.Messages.Add(msg);
            await _context.SaveChangesAsync();

            var createMessage = _mapper.Map<Message, MessageDto>(msg);
            await _hubContext.Clients.Group(room.Name).SendReviewAsync(createMessage);

            return Ok();  //CreatedAtAction(nameof(Get), new { id = msg.Id }, messageDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var message = await _context.Messages
                .Include(u => u.FromUser)
                .Where(m => m.Id == id && m.FromUser.UserName == User.Identity.Name)
                .FirstOrDefaultAsync();

            if (message == null)
                return NotFound();

            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
