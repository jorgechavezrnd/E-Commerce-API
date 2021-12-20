using AutoMapper;
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
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository repository, ILogger<CategoryService> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<CategoryDtoCollectionResponse> ListAsync(string filter, int page, int rows)
        {
            var response = new CategoryDtoCollectionResponse();

            try
            {
                var result = await _repository.ListAsync(filter ?? string.Empty, page, rows);

                response.Collection = result.collection
                    .Select(p => _mapper.Map<CategoryDto>(p))
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
                    response.ErrorMessage = Constants.NotFound;
                    return response;
                }

                response.Result = _mapper.Map<CategoryDto>(category);

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
                var category = _mapper.Map<Category>(request);

                response.Result = await _repository.CreateAsync(category);
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
                var category = _mapper.Map<Category>(request);
                category.Id = id;

                await _repository.UpdateAsync(category);

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
