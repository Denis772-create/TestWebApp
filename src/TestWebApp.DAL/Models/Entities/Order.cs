using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace TestWebApp.DAL.Models.DatabaseModels
{
    public class Order
    {
        [Key]
        public long Id { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime DateOfCreation { get; set; }
        
        [ForeignKey("User")]
        public virtual User User { get; set; }
    }
}
