using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.Filters;
using AutoMapper;
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
    public class RoomsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHubContext<ReviewHub, IReviewHub> _hubContext;
             
        public RoomsController(
            ApplicationDbContext context, 
            IMapper mapper,
            IHubContext<ReviewHub, IReviewHub> hubContext)
        {
            _context = context;
            _mapper = mapper;
            _hubContext = hubContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomDto>>> Get()
        {
            var rooms = await _context.Rooms.ToListAsync();

            var roomDto = _mapper.Map<IEnumerable<Room>, IEnumerable<RoomDto>>(rooms);

            return Ok(roomDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Room>> Get(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
                return NotFound();

            var roomViewModel = _mapper.Map<Room, RoomDto>(room);

            return Ok(roomViewModel);
        }


        [HttpPost("create")]
        [ValidateModel]
        public async Task<ActionResult<Room>> Create(RoomDto roomDto)
        {
            if (_context.Rooms.Any(u => u.Name == roomDto.Name))
                return BadRequest("Invalid room name or room already exists");

            var user = await _context.User.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
            var room = new Room
            {
                Name = roomDto.Name,
                Admin = user
            };

            await _context.Rooms.AddAsync(room);
            await _context.SaveChangesAsync();

            await _hubContext.Clients.All.SendInfoAsync($"id={room.Id}, name={room.Name}");

            return CreatedAtAction(nameof(Get), new {id = room.Id}, new {id = room.Id, name = room.Name});
        }

        [HttpPut("edit/{id}")]
        [ValidateModel]
        public async Task<IActionResult> Edit(int id, RoomDto roomDto)
        {
            if (_context.Rooms.Any(r => r.Name == roomDto.Name))
                return BadRequest("Invalid room name or room already exists");

            var room = await _context.Rooms
                .Include(r => r.Admin)
                .Where(r => r.Id == id && r.Admin.UserName == User.Identity.Name)
                .FirstOrDefaultAsync();

            if (room == null)
                return NotFound();

            room.Name = roomDto.Name;
            await _context.SaveChangesAsync();

            await _hubContext.Clients.All.SendInfoAsync($"The name of group success edit on {room.Name}.");

            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var room = await _context.Rooms
                .Include(r => r.Admin)
                .Where(r => r.Id == id && r.Admin.UserName == User.Identity.Name)
                .FirstOrDefaultAsync();

            if (room == null)
                return NotFound();

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();

            await _hubContext.Clients.All.SendInfoAsync("Room {0} has been deleted.\nYou are moved to the first available room!", room.Name);
 
            return NoContent();
        }

    }
}
