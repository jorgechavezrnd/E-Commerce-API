﻿using ECommerceAPI.DataAccess.Repositories;
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
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IProductRepository repository, ILogger<ProductService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<ProductDtoCollectionResponse> ListAsync(string filter, int page, int rows)
        {
            var response = new ProductDtoCollectionResponse();

            try
            {
                var tupla = await _repository
                    .GetCollectionAsync(filter ?? string.Empty, page, rows);

                response.Collection = tupla.collection
                    .Select(p => new ProductDto
                    {
                        Id = p.Id,
                        ProductName = p.ProductName,
                        ProductDescription = p.ProductDescription,
                        UrlProduct = p.UrlProduct,
                        UnitPrice = p.UnitPrice,
                        Category = p.Category
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
                _logger.LogCritical(ex.Message);
            }

            return response;
        }

        public async Task<BaseResponse<ProductSingleDto>> GetAsync(string id)
        {
            var response = new BaseResponse<ProductSingleDto>();

            try
            {
                var product = await _repository.GetItemAsync(id);

                if (product == null)
                {
                    response.Success = false;
                    response.ErrorMessage = "Registro no encontrado";
                    return response;
                }

                response.Result = new ProductSingleDto
                {
                    Id = product.Id,
                    ProductName = product.Name,
                    ProductDescription = product.Description,
                    CategoryId = product.CategoryId,
                    Active = product.Active,
                    UnitPrice = product.UnitPrice,
                    UrlProduct = product.ProductUrl
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

        public async Task<BaseResponse<string>> CreateAsync(ProductDtoRequest request)
        {
            var response = new BaseResponse<string>();

            try
            {
                var product = new Product
                {
                    Name = request.Name,
                    Description = request.Description,
                    Active = request.Active,
                    CategoryId = request.CategoryId,
                    ProductUrl = request.UrlImage,
                    UnitPrice = request.UnitPrice
                };

                response.Result = await _repository.CreateAsync(product);
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

        public async Task<BaseResponse<string>> UpdateAsync(string id, ProductDtoRequest request)
        {
            var response = new BaseResponse<string>();

            try
            {
                var product = new Product
                {
                    Name = request.Name,
                    Description = request.Description,
                    Active = request.Active,
                    CategoryId = request.CategoryId,
                    ProductUrl = request.UrlImage,
                    UnitPrice = request.UnitPrice,
                    Id = id
                };

                await _repository.UpdateAsync(product);

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
