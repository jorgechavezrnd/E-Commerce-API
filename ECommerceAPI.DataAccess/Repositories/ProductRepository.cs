using ECommerceAPI.Entities;
using ECommerceAPI.Entities.Complex;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceAPI.DataAccess.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ECommerceDbContext _dbContext;

        public ProductRepository(ECommerceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<(ICollection<ProductInfo> collection, int total)> GetCollectionAsync(string filter, int page, int rows)
        {
            var collection = await _dbContext.Set<Product>()
                .Where(p => p.Name.StartsWith(filter) && p.Status)
                .OrderBy(p => p.Name)
                .AsNoTracking()
                .Skip((page - 1) * rows)
                .Take(rows)
                .Select(p => new ProductInfo
                {
                    Id = p.Id,
                    ProductName = p.Name,
                    ProductDescription = p.Description,
                    Category = p.Category.Name,
                    UnitPrice = p.UnitPrice,
                    UrlProduct = p.ProductUrl
                })
                .ToListAsync();

            var totalCount = await _dbContext.Set<Product>()
                .Where(p => p.Name.StartsWith(filter) && p.Status)
                .AsNoTracking()
                .CountAsync();

            return (collection, totalCount);
        }

        public async Task<Product> GetItemAsync(string id)
        {
            return await _dbContext.Set<Product>()
                .SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<string> CreateAsync(Product entity)
        {
            await _dbContext.Set<Product>().AddAsync(entity);
            _dbContext.Entry(entity).State = EntityState.Added;
            await _dbContext.SaveChangesAsync();

            return entity.Id;
        }

        public async Task UpdateAsync(Product entity)
        {
            _dbContext.Set<Product>().Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var entity = await _dbContext.Set<Product>()
                .SingleOrDefaultAsync(p => p.Id == id);

            if (entity == null) return;

            _dbContext.Set<Product>().Remove(entity);
            _dbContext.Entry(entity).State = EntityState.Deleted;
            await _dbContext.SaveChangesAsync();
        }
    }
}
