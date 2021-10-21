using System.ComponentModel.DataAnnotations;

namespace TestWebApp.DAL.Models.Auth.Request
{
    public class RefreshRequest
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}