using System.ComponentModel.DataAnnotations;

namespace TestWebApp.DAL.Models.Auth.Request
{
    public class ProductRequest
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
    }
}
