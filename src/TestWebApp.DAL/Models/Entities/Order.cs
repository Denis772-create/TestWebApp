using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace TestWebApp.DAL.Models.Entities
{
    public class Order : BaseEntity
    {
        [Required(ErrorMessage = "Please, enter first name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please, enter last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please, enter country")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Please, enter phone number"), Phone]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please, enter email address"), EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please, enter total price")]
        public decimal TotalPrice { get; set; }
    }
}