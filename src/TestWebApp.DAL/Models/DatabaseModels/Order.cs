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

        [Required, DataType(DataType.Date)]
        public DateTime DateOfCreation { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime DateOfEditing { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual User User { get; set; }
    }
}
