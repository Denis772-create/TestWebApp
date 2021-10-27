using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWebApp.DAL.Models.Auth.Request
{
    public class ChangePasswordRequest
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password not specified")]
        [UIHint("Password")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Password not specified")]
        [UIHint("Password")]
        public string OldPassword { get; set; }
    }
}