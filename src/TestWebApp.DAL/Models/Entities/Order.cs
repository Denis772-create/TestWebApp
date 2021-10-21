using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace TestWebApp.DAL.Models.DatabaseModels
{
    public class Order
    {
        public Order()
        {
            DateOfCreation = DateTime.Now;
        }

        [Key]
        public long Id { get; set; }

        public DateTime DateOfCreation { get; set; }
        
        [ForeignKey("User")]
        public virtual User User { get; set; }
    }
}
