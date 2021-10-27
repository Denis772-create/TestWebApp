using System.ComponentModel.DataAnnotations;

namespace TestWebApp.DAL.Models.Auth.Requests
{
    public class RefreshRequest
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}