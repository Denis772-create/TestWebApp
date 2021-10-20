using System;
using System.ComponentModel.DataAnnotations;

namespace TestWebApp.DAL.Data
{
    public class Order
    {
        public long Id { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual User User { get; set; }
    }
}