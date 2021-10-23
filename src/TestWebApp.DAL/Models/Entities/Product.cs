using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TestWebApp.DAL.Models.Entities
{
    public class Product : BaseEntity
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

        public string ProductCategoryId { get; set; }

        [JsonIgnore]
        public ProductCategory ProductCategory { get; set; }
    }
}
