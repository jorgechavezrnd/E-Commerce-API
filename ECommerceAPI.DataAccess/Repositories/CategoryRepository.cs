using ECommerceAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceAPI.DataAccess.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ECommerceDbContext _dbContext;

        public CategoryRepository(ECommerceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<(ICollection<Category> collection, int total)> ListAsync(string filter, int page, int rows)
        {
            var collection = await _dbContext.Set<Category>()
                .Where(p => p.Name.StartsWith(filter) && p.Status)
                .OrderBy(p => p.Name)
                .AsNoTracking()
                .Skip((page - 1) * rows)
                .Take(rows)
                .ToListAsync();

            var totalCount = await _dbContext.Set<Category>()
                .Where(p => p.Name.StartsWith(filter) && p.Status)
                .AsNoTracking()
                .CountAsync();

            return (collection, totalCount);
        }

        public async Task<Category> GetItemAsync(string id)
        {
            return await _dbContext.Set<Category>()
                .SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<string> CreateAsync(Category entity)
        {
            await _dbContext.Set<Category>().AddAsync(entity);
            _dbContext.Entry(entity).State = EntityState.Added;
            await _dbContext.SaveChangesAsync();

            return entity.Id;
        }

        public async Task UpdateAsync(Category entity)
        {
            _dbContext.Set<Category>().Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var entity = await _dbContext.Set<Category>()
                .SingleOrDefaultAsync(p => p.Id == id);

            if (entity == null) return;

            _dbContext.Set<Category>().Remove(entity);
            _dbContext.Entry(entity).State = EntityState.Deleted;
            await _dbContext.SaveChangesAsync();
        }
    }
}
