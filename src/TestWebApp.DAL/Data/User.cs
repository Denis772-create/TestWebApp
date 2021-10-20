using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestWebApp.DAL.Data
{
    public class User : IdentityUser
    {
        public User()
        {
            Orders = new HashSet<Order>();
        }

        [Required]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required, Phone]
        public string Phone { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}