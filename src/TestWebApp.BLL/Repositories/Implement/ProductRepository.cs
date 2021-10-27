using TestWebApp.BLL.Repositories.Interfaces;
using TestWebApp.DAL.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestWebApp.DAL.Data;

namespace TestWebApp.BLL.Repositories.Implement
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var products = await _context.Products.AsNoTracking().Include(p => p.ProductCategory).ToListAsync();

            return products;
        }

        public async Task<Product> GetByIdAsync(string id)
        {
            var product = await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

            return product;
        }

        public async Task<bool> CreateAsync(Product product)
        {
            var isProductAvailable = await _context.Products
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p =>
                    p.Title == product.Title &&
                    p.Description == product.Description);

            if (product is not null && isProductAvailable is null)
            {
                await _context.Products.AddAsync(product);
                var created = await _context.SaveChangesAsync();

                return created > 0;
            }

            return false;
        }

        public async Task<bool> UpdateAsync(Product product)
        {
            if (product is not null)
            {
                _context.Products.Update(product);
                var updated = await _context.SaveChangesAsync();

                return updated > 0;
            }

            return false;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var product = await GetByIdAsync(id);

            if (product is not null)
            {
                _context.Products.Remove(product);
                var deleted = await _context.SaveChangesAsync();

                return deleted > 0;
            }

            return false;
        }
    }
}
