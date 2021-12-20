using AutoMapper;
using ECommerceAPI.Entities;
using ECommerceAPI.Entities.Complex;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ECommerceAPI.DataAccess.Repositories
{
    public class ProductRepository : ECommerceContextBase<Product>, IProductRepository
    {
        public ProductRepository(ECommerceDbContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        public async Task<(ICollection<ProductInfo> collection, int total)> GetCollectionAsync(string filter, int page, int rows)
        {
            Expression<Func<Product, bool>> predicate = p => p.Name.StartsWith(filter) && p.Status;

            Expression<Func<Product, ProductInfo>> selector = p => new ProductInfo
            {
                Id = p.Id,
                ProductName = p.Name,
                ProductDescription = p.Description,
                Category = p.Category.Name,
                UnitPrice = p.UnitPrice,
                UrlProduct = p.ProductUrl
            };

            Expression<Func<Product, string>> orderBy = p => p.Name;

            return await ListCollection(selector, predicate, orderBy, page, rows);
        }

        public async Task<Product> GetItemAsync(string id)
        {
            return await Context.Select<Product>(id);
        }

        public async Task<string> CreateAsync(Product entity)
        {
            return await Context.Insert(entity);
        }

        public async Task UpdateAsync(Product entity)
        {
            await Context.UpdateEntity(entity, Mapper);
        }

        public async Task DeleteAsync(string id)
        {
            await Context.Delete(new Product { Id = id });
        }
    }
}
