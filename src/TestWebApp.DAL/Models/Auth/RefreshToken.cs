using System.ComponentModel.DataAnnotations;
using System;

namespace TestWebApp.DAL.Models.Auth
{
    public class RefreshToken
    {
        public RefreshToken()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        [Required]
        public string Token { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}