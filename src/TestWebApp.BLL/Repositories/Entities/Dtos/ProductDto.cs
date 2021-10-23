using TestWebApp.DAL.Models.Entities;

namespace TestWebApp.BLL.Repositories.Entities.Dtos
{
    public class ProductDto
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int QuantityInStock { get; set; }

        public bool IsAvailable { get; set; }

        public string ProductCategoryId { get; set; }
        public ProductCategory ProductCategory { get; set; }
    }
}
