using TestWebApp.DAL.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestWebApp.BLL.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(string id);
        Task<bool> CreateAsync(Product product);
        Task<bool> UpdateAsync(Product product);
        Task<bool> DeleteAsync(string id);
    }
}
