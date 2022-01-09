using ECommerceAPI.Dto.Request;
using ECommerceAPI.Dto.Response;
using System.Threading.Tasks;

namespace ECommerceAPI.Services.Interfaces
{
    public interface IUserService
    {
        Task<RegisterUserDtoResponse> RegisterAsync(RegisterUserDtoRequest request);
        Task<LoginDtoResponse> LoginAsync(LoginDtoRequest request);
    }
}
