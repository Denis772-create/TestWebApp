
namespace TestWebApp.DAL.Models.Entities
{
    public class Order : BaseEntity
    {
        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
