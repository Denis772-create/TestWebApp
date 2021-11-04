using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using TestWebApp.DAL.Data;
using TestWebApp.DAL.DTO;
using TestWebApp.DAL.Models.ReviewModels;
using TestWebApp.ReviewServer.Hubs;
using TestWebApp.ReviewServer.Signatures;

namespace TestWebApp.ReviewServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly int _fileSizeLimit;
        private readonly string[] _allowedExtensions;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;
        private readonly IHubContext<ReviewHub, IReviewHub> _hubContext;

        public UploadController(
            ApplicationDbContext context,
            IMapper mapper,
            IWebHostEnvironment environment,
            IHubContext<ReviewHub, IReviewHub> hubContext,
            IConfiguration configruation)
        {
            _context = context;
            _mapper = mapper;
            _environment = environment;
            _hubContext = hubContext;

            _fileSizeLimit = configruation.GetSection("FileUpload")
                .GetValue<int>("FileSizeLimit");
            _allowedExtensions = configruation.GetSection("FileUpload")
                .GetValue<string>("AllowedExtensions").Split(",");
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload([FromForm] UploadDto uploadDto)
        {
            if (!ModelState.IsValid) return BadRequest();
            if (!Validate(uploadDto.File))
            {
                return BadRequest("Validation failed!");
            }

            var fileName = DateTime.Now.ToString("yyyymmddMMss") + "_" + Path.GetFileName(uploadDto.File.FileName);
            var folderPath = Path.Combine(_environment.WebRootPath, "uploads");
            var filePath = Path.Combine(folderPath, fileName);
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            await using var fileStream = new FileStream(filePath, FileMode.Create);

            await uploadDto.File.CopyToAsync(fileStream);


            var user = _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            var room = _context.Rooms.FirstOrDefault(r => r.Id == uploadDto.RoomId);
            if (user == null || room == null)
                return NotFound();

            string htmlImage = string.Format(
                "<a href=\"/uploads/{0}\" target=\"_blank\">" +
                "<img src=\"/uploads/{0}\" class=\"post-image\">" +
                "</a>", fileName);

            var message = new Message()
            {
                Content = Regex.Replace(htmlImage, @"(?i)<(?!img|a|/a|/img).*?>", string.Empty),
                Timestamp = DateTime.Now,
                FromUser = user,
                ToRoom = room
            };

            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();

            var messageViewModel = _mapper.Map<Message, MessageDto>(message);
            await _hubContext.Clients.Group(room.Name).SendReviewAsync(messageViewModel);

            return Ok();

        }

        private bool Validate(IFormFile file)
        {
            if (file.Length > _fileSizeLimit)
                return false;

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (string.IsNullOrEmpty(extension) || !_allowedExtensions.Any(s => s.Contains(extension)))
                return false;

            return true;
        }

    }
}
