using AutoMapper;
using ECommerceAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceAPI.DataAccess.Repositories
{
    public class CustomerRepository : ECommerceContextBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(ECommerceDbContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        public async Task<(ICollection<Customer> collection, int total)> GetCollectionAsync(string filter, int page, int rows)
        {
            return await ListCollection(p => p.Name.StartsWith(filter) && p.Status, page, rows);
        }

        public async Task<Customer> GetItemAsync(string id)
        {
            return await Context.Select<Customer>(id);
        }

        public async Task<Customer> GetItemByEmailAsync(string email)
        {
            return await Context.Set<Customer>()
                .Where(c => c.Email.Equals(email))
                .SingleOrDefaultAsync();
        }

        public async Task<string> CreateAsync(Customer entity)
        {
            return await Context.Insert(entity);
        }

        public async Task UpdateAsync(Customer entity)
        {
            await Context.UpdateEntity(entity, Mapper);
        }

        public async Task DeleteAsync(string id)
        {
            await Context.Delete(new Customer { Id = id });
        }
    }
}
