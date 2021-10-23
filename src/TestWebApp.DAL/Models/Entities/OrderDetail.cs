using System.ComponentModel.DataAnnotations;

namespace TestWebApp.DAL.Models.Entities
{
    public class OrderDetail : BaseEntity
    {
        [Required]
        public int Amount { get; set; }
       
        [Required]
        public decimal Price { get; set; }

        public string ProductId { get; set; }
        public virtual Product Product { get; set; }
        public string OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}
