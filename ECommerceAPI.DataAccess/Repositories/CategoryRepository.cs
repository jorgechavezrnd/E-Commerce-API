using AutoMapper;
using ECommerceAPI.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerceAPI.DataAccess.Repositories
{
    public class CategoryRepository : ECommerceContextBase<Category>, ICategoryRepository
    {
        public CategoryRepository(ECommerceDbContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        public async Task<(ICollection<Category> collection, int total)> ListAsync(string filter, int page, int rows)
        {
            return await ListCollection(
                c => c,
                c => c.Name.StartsWith(filter),
                c => c.Description,
                page,
                rows);
        }

        public async Task<Category> GetItemAsync(string id)
        {
            return await Context.Select<Category>(id);
        }

        public async Task<string> CreateAsync(Category entity)
        {
            return await Context.Insert(entity);
        }

        public async Task UpdateAsync(Category entity)
        {
            await Context.UpdateEntity(entity, Mapper);
        }

        public async Task DeleteAsync(string id)
        {
            await Context.Delete(new Category { Id = id });
        }
    }
}
