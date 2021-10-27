using System.ComponentModel.DataAnnotations;

namespace TestWebApp.DAL.Models.Auth.Request
{
    public class LoginRequest
    {
        public LoginRequest()
        {
            RememberMe = true;
        }

        [EmailAddress]
        [Required(ErrorMessage = "Email not specified")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password not specified")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}