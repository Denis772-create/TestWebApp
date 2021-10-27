using TestWebApp.BLL.Repositories.Interfaces;
using TestWebApp.DAL.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestWebApp.DAL.Data;

namespace TestWebApp.BLL.Repositories.Implement
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            var orders = await _context.Orders.AsNoTracking().ToListAsync();

            return orders;
        }

        public async Task<Order> GetByIdAsync(string id)
        {
            var order = await _context.Orders.AsNoTracking().FirstOrDefaultAsync(o => o.Id == id);

            return order;
        }

        public async Task<bool> CreateAsync(Order order)
        {
            if (order is not null)
            {
                await _context.Orders.AddAsync(order);
                var created = await _context.SaveChangesAsync();

                return created > 0;
            }

            return false;
        }

        public async Task<bool> UpdateAsync(Order order)
        {
            if (order is not null)
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

            if (order is not null)
            {
                _context.Orders.Remove(order);
                var deleted = await _context.SaveChangesAsync();

                return deleted > 0;
            }

            return false;
        }
    }
}
