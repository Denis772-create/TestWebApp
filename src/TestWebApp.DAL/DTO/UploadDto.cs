using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace TestWebApp.DAL.DTO
{
    public class UploadDto
    {
        [Required]
        public int RoomId { get; set; }
        [Required]
        public IFormFile File { get; set; }
    }
}
