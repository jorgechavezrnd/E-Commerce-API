using ECommerceAPI.Entities;
using ECommerceAPI.Entities.Complex;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerceAPI.DataAccess.Repositories
{
    public interface ISaleRepository
    {
        Task<(ICollection<InvoiceInfo> collection, int total)> SelectAsync(string dni, int page, int rows);
        Task<(ICollection<InvoiceInfo> collection, int total)> SelectAsync(DateTime dateInit, DateTime dateEnd, int page, int rows);
        Task<(ICollection<InvoiceInfo> collection, int total)> SelectByInvoiceNumber(string invoiceNumber, int page, int rows);
        Task<Sale> CreateAsync(Sale entity);
        Task CreateSaleDetail(SaleDetail entity);
        Task CommitTransaction();
        Task RollbackTransaction();
    }
}
