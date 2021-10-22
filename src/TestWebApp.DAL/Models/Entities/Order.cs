using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace TestWebApp.DAL.Models.Entities
{
    public class Order : BaseEntity
    {
        public string UserId { get; set; }
        public virtual User User { get; set; }

        public string ProductId { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}