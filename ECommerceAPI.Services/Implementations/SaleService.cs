using AutoMapper;
using ECommerceAPI.DataAccess.Repositories;
using ECommerceAPI.Dto.Request;
using ECommerceAPI.Dto.Response;
using ECommerceAPI.Entities;
using ECommerceAPI.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceAPI.Services.Implementations
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _repository;
        private readonly ILogger<SaleService> _logger;
        private readonly IMapper _mapper;

        public SaleService(ISaleRepository repository,
            ILogger<SaleService> logger,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<BaseCollectionResponse<SaleDtoSingleResponse>> SelectByDni(string dni, int page, int rows)
        {
            var response = new BaseCollectionResponse<SaleDtoSingleResponse>();

            try
            {
                var tuple = await _repository.SelectAsync(dni ?? string.Empty, page, rows);

                response.Collection = tuple.collection
                    .Select(p => _mapper.Map<SaleDtoSingleResponse>(p))
                    .ToList();

                response.TotalPages = ECommerceUtils.GetTotalPages(tuple.total, rows);
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

        public async Task<BaseCollectionResponse<SaleDtoSingleResponse>> SelectByInvoiceNumber(string number, int page, int rows)
        {
            var response = new BaseCollectionResponse<SaleDtoSingleResponse>();

            try
            {
                var tuple = await _repository.SelectByInvoiceNumber(number ?? string.Empty, page, rows);

                response.Collection = tuple.collection
                    .Select(p => _mapper.Map<SaleDtoSingleResponse>(p))
                    .ToList();

                response.TotalPages = ECommerceUtils.GetTotalPages(tuple.total, rows);
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

        public async Task<BaseCollectionResponse<SaleDtoSingleResponse>> SelectByDate(DateTime dateInit, DateTime dateEnd, int page, int rows)
        {
            var response = new BaseCollectionResponse<SaleDtoSingleResponse>();

            try
            {
                var tuple = await _repository.SelectAsync(dateInit, dateEnd, page, rows);

                response.Collection = tuple.collection
                    .Select(p => _mapper.Map<SaleDtoSingleResponse>(p))
                    .ToList();

                response.TotalPages = ECommerceUtils.GetTotalPages(tuple.total, rows);
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

        public async Task<BaseCollectionResponse<SaleDetailSingleResponse>> SelectDetailsAsync(string saleId)
        {
            var response = new BaseCollectionResponse<SaleDetailSingleResponse>();

            try
            {
                var collection = await _repository.GetSaleDetails(saleId);

                response.Collection = collection
                    .Select(p => _mapper.Map<SaleDetailSingleResponse>(p))
                    .ToList();

                response.Success = collection.Any();
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                response.Success = false;
                response.ErrorMessage = ex.Message;
            }

            return response;
        }

        public async Task<BaseResponse<string>> CreateAsync(SaleDtoRequest request)
        {
            var response = new BaseResponse<string>();

            try
            {
                var entity = new Sale
                {
                    CustomerId = request.CustomerId,
                    PaymentMethod = (PaymentMethodEnum)request.PaymentMethod,
                    SaleDate = Convert.ToDateTime(request.Date),
                    TotalSale = request.Products.Sum(x => x.Quantity * x.UnitPrice)
                };

                var sale = await _repository.CreateAsync(entity);

                var counter = 0;
                foreach (var product in request.Products)
                {
                    counter++;
                    var saleDetail = _mapper.Map<SaleDetail>(product);
                    saleDetail.ItemNumber = counter;
                    saleDetail.Total = saleDetail.Quantity * saleDetail.UnitPrice;
                    saleDetail.SaleId = sale.Id;

                    await _repository.CreateSaleDetail(saleDetail);
                }

                await _repository.CommitTransaction();

                response.Result = sale.Id;
                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                response.Success = false;
                response.ErrorMessage = ex.Message;
                await _repository.RollbackTransaction();
            }

            return response;
        }

        public async Task<BaseResponse<ICollection<ReportByMonthSingleDto>>> ReportByMonth(int month, int year)
        {
            var response = new BaseResponse<ICollection<ReportByMonthSingleDto>>();

            try
            {
                var collection = await _repository.GetReportByMonth(month, year);
                response.Result = collection
                    .Select(p => new ReportByMonthSingleDto
                    {
                        Day = p.Day,
                        TotalSales = p.TotalSales
                    })
                    .ToList();

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
