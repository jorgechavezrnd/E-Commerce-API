using AutoMapper;
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
        private readonly IMapper _mapper;

        public ProductRepository(ECommerceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
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
            return await _dbContext.Select<Product>(id);
        }

        public async Task<string> CreateAsync(Product entity)
        {
            return await _dbContext.Insert(entity);
        }

        public async Task UpdateAsync(Product entity)
        {
            await _dbContext.UpdateEntity(entity, _mapper);
        }

        public async Task DeleteAsync(string id)
        {
            await _dbContext.Delete(new Product { Id = id });
        }
    }
}
