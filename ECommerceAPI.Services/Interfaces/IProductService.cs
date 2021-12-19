using ECommerceAPI.Dto.Request;
using ECommerceAPI.Dto.Response;
using System.Threading.Tasks;

namespace ECommerceAPI.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductDtoCollectionResponse> ListAsync(string filter, int page, int rows);

        Task<BaseResponse<ProductSingleDto>> GetAsync(string id);

        Task<BaseResponse<string>> CreateAsync(ProductDtoRequest request);

        Task<BaseResponse<string>> UpdateAsync(string id, ProductDtoRequest request);

        Task<BaseResponse<string>> DeleteAsync(string id);
    }
}
