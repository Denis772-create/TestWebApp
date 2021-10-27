using TestWebApp.DAL.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestWebApp.BLL.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllAsync();
        Task<Order> GetByIdAsync(string id);
        Task<bool> CreateAsync(Order product);
        Task<bool> UpdateAsync(Order product);
        Task<bool> DeleteAsync(string id);
    }
}
