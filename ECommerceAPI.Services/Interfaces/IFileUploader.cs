using System.Threading.Tasks;

namespace ECommerceAPI.Services.Interfaces
{
    public interface IFileUploader
    {
        Task<string> UploadAsync(string base64String, string fileName);
    }
}
