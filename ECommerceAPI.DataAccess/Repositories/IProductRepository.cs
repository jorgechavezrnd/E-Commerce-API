using ECommerceAPI.Entities;
using ECommerceAPI.Entities.Complex;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerceAPI.DataAccess.Repositories
{
    public interface IProductRepository
    {
        Task<(ICollection<ProductInfo> collection, int total)> GetCollectionAsync(string filter, int page, int rows);

        Task<Product> GetItemAsync(string id);

        Task<string> CreateAsync(Product entity);

        Task UpdateAsync(Product entity);

        Task DeleteAsync(string id);
    }
}
