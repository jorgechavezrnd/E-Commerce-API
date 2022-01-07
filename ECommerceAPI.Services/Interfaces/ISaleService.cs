using ECommerceAPI.Dto.Request;
using ECommerceAPI.Dto.Response;
using System;
using System.Threading.Tasks;

namespace ECommerceAPI.Services.Interfaces
{
    public interface ISaleService
    {
        Task<BaseCollectionResponse<SaleDtoSingleResponse>> SelectByDni(string dni, int page, int rows);
        Task<BaseCollectionResponse<SaleDtoSingleResponse>> SelectByInvoiceNumber(string number, int page, int rows);
        Task<BaseCollectionResponse<SaleDtoSingleResponse>> SelectByDate(DateTime dateInit, DateTime dateEnd, int page, int rows);
        Task<BaseCollectionResponse<SaleDetailSingleResponse>> SelectDetailsAsync(string saleId);
        Task<BaseResponse<string>> CreateAsync(SaleDtoRequest request);
    }
}
