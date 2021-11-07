using ECommerceAPI.Dto.Request;
using ECommerceAPI.Dto.Response;
using System.Threading.Tasks;

namespace ECommerceAPI.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryDtoCollectionResponse> ListAsync(string filter, int page, int rows);

        Task<BaseResponse<CategoryDto>> GetAsync(string id);

        Task<BaseResponse<string>> CreateAsync(CategoryRequest request);

        Task<BaseResponse<string>> UpdateAsync(string id, CategoryRequest request);

        Task<BaseResponse<string>> DeleteAsync(string id);
    }
}
