using System.ComponentModel.DataAnnotations;
<<<<<<< HEAD

<<<<<<< HEAD

using System.Collections.Generic;
=======
>>>>>>> 21212d4d5ad37743867782b343813b6489e6fb3e

=======
>>>>>>> 21212d4d5ad37743867782b343813b6489e6fb3e

namespace TestWebApp.DAL.Models.Entities
{
    public class Product : BaseEntity
    {
<<<<<<< HEAD
=======
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

        public string ProductCategoryId { get; set; }
        public ProductCategory ProductCategory { get; set; }
>>>>>>> 21212d4d5ad37743867782b343813b6489e6fb3e
    }
}