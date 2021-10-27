using System.ComponentModel.DataAnnotations;
using TestWebApp.DAL.Models.Entities;

namespace TestWebApp.DAL.Models.Auth.Request
{
    public class ProductRequest
    {
        [Required(ErrorMessage = "Please, enter product title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please, enter product description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please, enter product price")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Please, enter how many products are in stock")]
        public int QuantityInStock { get; set; }

        [Required(ErrorMessage = "Please, enter if the product is in stock")]
        public bool IsAvailable { get; set; }

        [Required(ErrorMessage = "Please, enter product category")]
        public string ProductCategoryName { get; set; }
    }
}