using ECommerceAPI.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerceAPI.DataAccess.Repositories
{
    public interface IProductRepository
    {
        Task<(ICollection<Product> collection, int total)> ListAsync(string filter, int page, int rows);

        Task<Product> GetItemAsync(string id);

        Task<string> CreateAsync(Product entity);

        Task UpdateAsync(Product entity);

        Task DeleteAsync(string id);
    }
}
