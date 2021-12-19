using ECommerceAPI.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerceAPI.DataAccess.Repositories
{
    public interface ICustomerRepository
    {
        Task<(ICollection<Customer> collection, int total)> GetCollectionAsync(string filter, int page, int rows);

        Task<Customer> GetItemAsync(string id);

        Task<Customer> GetItemByEmailAsync(string email);

        Task<string> CreateAsync(Customer entity);

        Task UpdateAsync(Customer entity);

        Task DeleteAsync(string id);
    }
}
