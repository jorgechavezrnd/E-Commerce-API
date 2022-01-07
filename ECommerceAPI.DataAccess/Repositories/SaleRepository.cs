using AutoMapper;
using ECommerceAPI.Entities;
using ECommerceAPI.Entities.Complex;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ECommerceAPI.DataAccess.Repositories
{
    public class SaleRepository : ECommerceContextBase<Sale>, ISaleRepository
    {
        public SaleRepository(ECommerceDbContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        private static Expression<Func<Sale, InvoiceInfo>> GetSelector()
        {
            return p => new InvoiceInfo
            {
                Id = p.Id,
                CustomerName = p.Customer.LastName,
                InvoiceNumber = p.InvoiceNumber,
                SaleDate = p.SaleDate.ToString(Constants.DateFormat),
                TotalAmount = p.TotalSale
            };
        }

        public async Task<(ICollection<InvoiceInfo> collection, int total)> SelectAsync(string dni, int page, int rows)
        {
            return await ListCollection(GetSelector(), x => x.Customer.Dni == dni, page, rows);
        }

        public async Task<(ICollection<InvoiceInfo> collection, int total)> SelectAsync(DateTime dateInit, DateTime dateEnd, int page, int rows)
        {
            return await ListCollection(GetSelector(),
                p => dateInit <= p.SaleDate
                     && dateEnd >= p.SaleDate,
                page, rows);
        }

        public async Task<(ICollection<InvoiceInfo> collection, int total)> SelectByInvoiceNumber(string invoiceNumber, int page, int rows)
        {
            return await ListCollection(GetSelector(),
                p => p.InvoiceNumber.Contains(invoiceNumber),
                page, rows);
        }

        public async Task<Sale> CreateAsync(Sale entity)
        {
            var number = await Context.Set<Sale>().CountAsync() + 1;
            entity.InvoiceNumber = $"F{number:0000}";

            await Context.Database.BeginTransactionAsync();

            await Context.Set<Sale>().AddAsync(entity);
            Context.Entry(entity).State = EntityState.Added;
            return entity;
        }

        public async Task<ICollection<InvoiceDetailInfo>> GetSaleDetails(string saleId)
        {
            return await Context.Set<SaleDetail>()
                .Where(p => p.SaleId == saleId)
                .Select(p => new InvoiceDetailInfo
                {
                    Id = p.Id,
                    ItemNumber = p.ItemNumber,
                    ProductName = p.Product.Name,
                    Quantity = p.Quantity,
                    UnitPrice = p.UnitPrice,
                    Total = p.Total
                })
                .ToListAsync();
        }

        public async Task CreateSaleDetail(SaleDetail entity)
        {
            await Context.Set<SaleDetail>().AddAsync(entity);
            Context.Entry(entity).State = EntityState.Added;
        }

        public async Task CommitTransaction()
        {
            await Context.SaveChangesAsync();
            await Context.Database.CommitTransactionAsync();
        }

        public async Task RollbackTransaction()
        {
            await Context.Database.RollbackTransactionAsync();
        }
    }
}
