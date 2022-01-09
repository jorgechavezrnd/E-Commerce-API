using ECommerceAPI.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using ECommerceAPI.Entities;
using System;
using System.IO;

namespace ECommerceAPI.Services.Implementations
{
    public class FileUploader : IFileUploader
    {
        private readonly IOptions<AppSettings> _options;
        private readonly ILogger<FileUploader> _logger;

        public FileUploader(IOptions<AppSettings> options, ILogger<FileUploader> logger)
        {
            _options = options;
            _logger = logger;
        }

        public async Task<string> UploadAsync(string base64String, string fileName)
        {
            try
            {
                var bytes = Convert.FromBase64String(base64String);
                // D:\Servidor\Imagenes\ps4.jpg
                var path = Path.Combine(_options.Value.StorageConfiguration.Path, fileName);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await fileStream.WriteAsync(bytes, 0, bytes.Length);
                }

                // http://localhost/ecommercepictures/ps4.jpg

                return $"{_options.Value.StorageConfiguration.PublicUrl}{fileName}";
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Error en ", ex.Message);
                return string.Empty;
            }
        }
    }
}
