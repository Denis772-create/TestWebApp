using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace TestWebApp.DAL.Models.Entities
{
    public class Product : BaseEntity
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int QuantityInStock { get; set; }

        [Required]
        public bool IsAvailable { get; set; }

        public virtual ProductCategory ProductCategory { get; set; }
    }
}
