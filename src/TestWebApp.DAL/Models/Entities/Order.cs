using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TestWebApp.DAL.Models.Entities
{
    public class Order : BaseEntity
    {
        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}