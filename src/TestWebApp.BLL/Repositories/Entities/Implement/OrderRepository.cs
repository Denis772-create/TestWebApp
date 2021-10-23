using TestWebApp.BLL.Repositories.Entities.Interfaces;
using TestWebApp.DAL.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestWebApp.DAL.Data;

namespace TestWebApp.BLL.Repositories.Entities.Implement
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            var orders = await _context.Orders.AsNoTracking().Include(o => o.Product).Include(o => o.User).ToListAsync();

            return orders;
        }

        public async Task<Order> GetByIdAsync(string id)
        {
            var order = await _context.Orders.AsNoTracking().FirstOrDefaultAsync(o => o.Id == id);

            return order;
        }

        public async Task<bool> CreateAsync(Order order)
        {
            var isOrderAvailable = await GetByIdAsync(order.Id);

            if (order != null && isOrderAvailable == null)
            {
                await _context.Orders.AddAsync(order);
                var created = await _context.SaveChangesAsync();

                return created > 0;
            }

            return false;
        }

        public async Task<bool> UpdateAsync(Order order)
        {
            if (order != null)
            {
                _context.Orders.Update(order);
                var updated = await _context.SaveChangesAsync();

                return updated > 0;
            }

            return false;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var order = await GetByIdAsync(id);

            if (order != null)
            {
                _context.Orders.Remove(order);
                var deleted = await _context.SaveChangesAsync();

                return deleted > 0;
            }

            return false;
        }
    }
}
