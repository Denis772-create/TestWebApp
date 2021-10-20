using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace TestWebApp.DAL.Models.DatabaseModels
{
    public class User : IdentityUser
    {
        public User()
        {
            Orders = new List<Order>();
        }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required, Phone]
        public string Phone { get; set; }

        [ForeignKey("Orders")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
