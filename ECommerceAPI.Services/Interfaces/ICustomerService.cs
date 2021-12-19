using ECommerceAPI.Dto.Request;
using ECommerceAPI.Dto.Response;
using System.Threading.Tasks;

namespace ECommerceAPI.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerDtoCollectionResponse> GetCollectionAsync(string filter, int page, int rows);

        Task<BaseResponse<CustomerDto>> GetCustomerAsync(string id);

        Task<BaseResponse<string>> CreateAsync(CustomerDtoRequest request);

        Task<BaseResponse<string>> UpdateAsync(string id, CustomerDtoRequest request);

        Task<BaseResponse<string>> DeleteAsync(string id);
    }
}
