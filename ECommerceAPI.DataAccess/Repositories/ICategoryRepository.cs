using ECommerceAPI.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerceAPI.DataAccess.Repositories
{
    public interface ICategoryRepository
    {
        Task<(ICollection<Category> collection, int total)> ListAsync(string filter, int page, int rows);

        Task<Category> GetItemAsync(string id);

        Task<string> CreateAsync(Category entity);

        Task UpdateAsync(Category entity);

        Task DeleteAsync(string id);
    }
}
