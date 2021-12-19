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
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(ICustomerRepository repository, ILogger<CustomerService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<CustomerDtoCollectionResponse> GetCollectionAsync(string filter, int page, int rows)
        {
            var response = new CustomerDtoCollectionResponse();

            try
            {
                var tupla = await _repository
                    .GetCollectionAsync(filter ?? string.Empty, page, rows);

                response.Collection = tupla.collection
                    .Select(c => new CustomerDto
                    {
                        Id = c.Id,
                        Name = c.Name,
                        LastName = c.LastName,
                        Email = c.Email,
                        BirthDate = c.BirthDate.ToString("yyyy-MM-dd"),
                        Dni = c.Dni
                    })
                    .ToList();

                var totalPages = tupla.total / rows;
                if (tupla.total % rows > 0)
                    totalPages++;

                response.TotalPages = totalPages;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
                _logger.LogCritical($"{ex.Message} {ex.StackTrace}");
            }

            return response;
        }

        public async Task<BaseResponse<CustomerDto>> GetCustomerAsync(string id)
        {
            var response = new BaseResponse<CustomerDto>();

            try
            {
                var customer = await _repository.GetItemAsync(id);

                if (customer == null)
                {
                    response.Success = true;
                    response.ErrorMessage = "Registro no encontrado";
                    return response;
                }

                response.Result = new CustomerDto
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    LastName = customer.LastName,
                    Email = customer.Email,
                    BirthDate = customer.BirthDate.ToString("yyyy-MM-dd"),
                    Dni = customer.Dni
                };

                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Result = null;
                response.ErrorMessage = ex.Message;
                _logger.LogCritical(ex.Message);
            }

            return response;
        }

        public async Task<BaseResponse<string>> CreateAsync(CustomerDtoRequest request)
        {
            var response = new BaseResponse<string>();

            try
            {
                var customer = new Customer
                {
                    Name = request.Name,
                    LastName = request.LastName,
                    Email = request.Email,
                    BirthDate = DateTime.ParseExact(request.BirthDate, "yyyy-MM-dd", null),
                    Dni = request.Dni
                };

                response.Result = await _repository.CreateAsync(customer);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Result = null;
                response.ErrorMessage = ex.Message;
                _logger.LogCritical(ex.Message);
            }

            return response;
        }

        public async Task<BaseResponse<string>> UpdateAsync(string id, CustomerDtoRequest request)
        {
            var response = new BaseResponse<string>();

            try
            {
                var customer = new Customer
                {
                    Name = request.Name,
                    LastName = request.LastName,
                    Email = request.Email,
                    BirthDate = DateTime.ParseExact(request.BirthDate, "yyyy-MM-dd", null),
                    Dni = request.Dni,
                    Id = id
                };

                await _repository.UpdateAsync(customer);

                response.Success = true;
                response.Result = id;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Result = null;
                response.ErrorMessage = ex.Message;
                _logger.LogCritical(ex.Message);
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
                response.Success = false;
                response.Result = null;
                response.ErrorMessage = ex.Message;
                _logger.LogCritical(ex.Message);
            }

            return response;
        }
    }
}
