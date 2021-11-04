﻿using System.ComponentModel.DataAnnotations;

namespace TestWebApp.DAL.DTO
{
    public class MessageDto
    {
        [Required]
        public string Content { get; set; }
        public string Timestamp { get; set; }
        public string From { get; set; }
        [Required]
        public string Room { get; set; }
        public string Avatar { get; set; }
    }
}
