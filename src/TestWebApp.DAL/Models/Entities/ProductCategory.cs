using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace TestWebApp.DAL.Models.Entities
{
    public class ProductCategory : BaseEntity
    {
        [Required]
        public string ProductName { get; set; }

        public ICollection<Product> Products { get; set; }

        public ProductCategory()
        {
            Products = new List<Product>();
        }
    }
}
