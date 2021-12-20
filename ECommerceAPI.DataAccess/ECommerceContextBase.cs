using AutoMapper;
using ECommerceAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ECommerceAPI.DataAccess
{
    public class ECommerceContextBase<TEntityBase>
        where TEntityBase : EntityBase, new()
    {
        protected readonly ECommerceDbContext Context;
        protected readonly IMapper Mapper;

        protected ECommerceContextBase(ECommerceDbContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }

        protected async Task<(ICollection<TEntityBase> collection, int total)> ListCollection(
            Expression<Func<TEntityBase, bool>> predicate, int page, int rows)
        {
            var collection = await Context.Set<TEntityBase>()
                .Where(predicate)
                .OrderBy(p => p.Status)
                .AsNoTracking()
                .Skip((page - 1) * rows)
                .Take(rows)
                .ToListAsync();

            var totalCount = await Context.Set<TEntityBase>()
                .Where(predicate)
                .AsNoTracking()
                .CountAsync();

            return (collection, totalCount);
        }

        protected async Task<(ICollection<TInfo> collection, int total)> ListCollection<TInfo>(
            Expression<Func<TEntityBase, TInfo>> selector,
            Expression<Func<TEntityBase, bool>> predicate,
            int page,
            int rows)
        {
            var collection = await Context.Set<TEntityBase>()
                .Where(predicate)
                .OrderBy(p => p.Status)
                .AsNoTracking()
                .Select(selector)
                .Skip((page - 1) * rows)
                .Take(rows)
                .ToListAsync();

            var totalCount = await Context.Set<TEntityBase>()
                .Where(predicate)
                .AsNoTracking()
                .CountAsync();

            return (collection, totalCount);
        }

        protected async Task<(ICollection<TInfo> collection, int total)> ListCollection<TInfo, TOrderBy>(
            Expression<Func<TEntityBase, TInfo>> selector,
            Expression<Func<TEntityBase, bool>> predicate,
            Expression<Func<TEntityBase, TOrderBy>> ordenamiento,
            int page,
            int rows)
        {
            var collection = await Context.Set<TEntityBase>()
                .Where(predicate)
                .OrderBy(ordenamiento)
                .AsNoTracking()
                .Select(selector)
                .Skip((page - 1) * rows)
                .Take(rows)
                .ToListAsync();

            var totalCount = await Context.Set<TEntityBase>()
                .Where(predicate)
                .AsNoTracking()
                .CountAsync();

            return (collection, totalCount);
        }
    }
}
