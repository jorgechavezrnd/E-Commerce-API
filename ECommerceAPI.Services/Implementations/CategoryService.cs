using ECommerceAPI.DataAccess.Repositories;
using ECommerceAPI.Dto.Request;
using ECommerceAPI.Dto.Response;
using ECommerceAPI.Entities;
using ECommerceAPI.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceAPI.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(ICategoryRepository repository, ILogger<CategoryService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<CategoryDtoCollectionResponse> ListAsync(string filter, int page, int rows)
        {
            var response = new CategoryDtoCollectionResponse();

            try
            {
                var result = await _repository.ListAsync(filter ?? string.Empty, page, rows);

                response.Collection = result.collection
                    .Select(p => new CategoryDto
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Description = p.Description
                    })
                    .ToList();

                response.TotalPages = result.total / rows;
                if (result.total % rows > 0)
                {
                    response.TotalPages++;
                }

                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                response.Success = false;
                response.ErrorMessage = ex.Message;
            }

            return response;
        }

        public async Task<BaseResponse<CategoryDto>> GetAsync(string id)
        {
            var response = new BaseResponse<CategoryDto>();

            try
            {
                var category = await _repository.GetItemAsync(id);
                if (category == null)
                {
                    response.Success = false;
                    response.ErrorMessage = "Registro no encontrado";
                    return response;
                }

                response.Result = new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description
                };

                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                response.Success = false;
                response.ErrorMessage = ex.Message;
            }

            return response;
        }

        public async Task<BaseResponse<string>> CreateAsync(CategoryRequest request)
        {
            var response = new BaseResponse<string>();

            try
            {
                response.Result = await _repository.CreateAsync(new Category
                {
                    Name = request.Name,
                    Description = request.Description
                });

                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                response.Success = false;
                response.ErrorMessage = ex.Message;
            }

            return response;
        }

        public async Task<BaseResponse<string>> UpdateAsync(string id, CategoryRequest request)
        {
            var response = new BaseResponse<string>();

            try
            {
                await _repository.UpdateAsync(new Category
                {
                    Id = id,
                    Name = request.Name,
                    Description = request.Description
                });

                response.Result = id;
                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                response.Success = false;
                response.ErrorMessage = ex.Message;
            }

            return response;
        }

        public async Task<BaseResponse<string>> DeleteAsync(string id)
        {
            var response = new BaseResponse<string>();

            try
            {
                await _repository.DeleteAsync(id);

                response.Result = id;
                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                response.Success = false;
                response.ErrorMessage = ex.Message;
            }

            return response;
        }
    }
}
