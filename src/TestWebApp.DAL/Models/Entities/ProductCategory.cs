using System.ComponentModel.DataAnnotations;

namespace TestWebApp.DAL.Models.Entities
{
    public class ProductCategory : BaseEntity
    {
        [Required]
        public string ProductName { get; set; }
    }
}
