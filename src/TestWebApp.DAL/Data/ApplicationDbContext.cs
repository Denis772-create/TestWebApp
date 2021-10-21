using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using TestWebApp.DAL.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace TestWebApp.DAL.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<User> User { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=..\\TestWebApp.DAL\\App_DB\\ShopDB.db");
        }
    }
}