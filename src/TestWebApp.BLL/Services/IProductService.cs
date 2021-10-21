using TestWebApp.DAL.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestWebApp.BLL.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(long id);
        Task CreateAsync(Product product);
        Task UpdateAsync(Product product);
        Task<bool> Delete(long id);
    }
}
