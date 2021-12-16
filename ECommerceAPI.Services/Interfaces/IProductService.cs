using ECommerceAPI.Dto.Request;
using ECommerceAPI.Dto.Response;
using System.Threading.Tasks;

namespace ECommerceAPI.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductDtoCollectionResponse> ListAsync(string filter, int page, int rows);

        Task<BaseResponse<ProductDto>> GetAsync(string id);

        Task<BaseResponse<string>> CreateAsync(ProductRequest request);

        Task<BaseResponse<string>> UpdateAsync(string id, ProductRequest request);

        Task<BaseResponse<string>> DeleteAsync(string id);
    }
}
