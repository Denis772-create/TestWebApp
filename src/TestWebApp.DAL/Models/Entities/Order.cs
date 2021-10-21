using System.ComponentModel.DataAnnotations.Schema;

namespace TestWebApp.DAL.Models.Entities
{
    public class Order : BaseEntity
    {
        [ForeignKey("User")]
        public virtual User User { get; set; }
    }
}