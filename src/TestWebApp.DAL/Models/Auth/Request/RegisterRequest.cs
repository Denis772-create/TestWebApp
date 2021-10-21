using System.ComponentModel.DataAnnotations;

namespace TestWebApp.DAL.Models.Auth
{
    public class RegisterRequest
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email not specified")]
        [UIHint("Email"), EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password not specified")]
        [UIHint("Password")]
        public string Password { get; set; }

        [UIHint("Password")]
        [Compare("Password", ErrorMessage = "Password entered incorrectly")]
        public string ConfirmPassword { get; set; }
    }
}