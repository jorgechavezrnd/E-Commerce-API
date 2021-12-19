using ECommerceAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceAPI.DataAccess.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ECommerceDbContext _dbContext;

        public CustomerRepository(ECommerceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<(ICollection<Customer> collection, int total)> GetCollectionAsync(string filter, int page, int rows)
        {
            var collection = await _dbContext.Set<Customer>()
                .Where(c => c.Name.StartsWith(filter) && c.Status)
                .OrderBy(c => c.Name)
                .AsNoTracking()
                .Skip((page - 1) * rows)
                .Take(rows)
                .ToListAsync();

            var totalCount = await _dbContext.Set<Customer>()
                .Where(c => c.Name.StartsWith(filter) && c.Status)
                .AsNoTracking()
                .CountAsync();

            return (collection, totalCount);
        }

        public async Task<Customer> GetItemAsync(string id)
        {
            return await _dbContext.Set<Customer>()
                .SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Customer> GetItemByEmailAsync(string email)
        {
            return await _dbContext.Set<Customer>()
                .Where(c => c.Email.Equals(email))
                .SingleOrDefaultAsync();
        }

        public async Task<string> CreateAsync(Customer entity)
        {
            await _dbContext.Set<Customer>().AddAsync(entity);
            _dbContext.Entry(entity).State = EntityState.Added;
            await _dbContext.SaveChangesAsync();

            return entity.Id;
        }

        public async Task UpdateAsync(Customer entity)
        {
            _dbContext.Set<Customer>().Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var entity = await _dbContext.Set<Customer>()
                .SingleOrDefaultAsync(c => c.Id == id);

            if (entity == null) return;
            entity.Status = false;

            _dbContext.Set<Customer>().Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
