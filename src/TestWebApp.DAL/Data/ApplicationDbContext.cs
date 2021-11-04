using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using TestWebApp.DAL.Models.Entities;
using Microsoft.EntityFrameworkCore;
using TestWebApp.DAL.Models;
using TestWebApp.DAL.Models.ReviewModels;

namespace TestWebApp.DAL.Data
{
    public sealed class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<User> User { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Message> Messages { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=..\\src\\TestWebApp.DAL\\App_DB\\ShopDB.db");
        }
    }
}