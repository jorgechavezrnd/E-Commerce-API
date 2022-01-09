using ECommerceAPI.Dto.Request;
using ECommerceAPI.Dto.Response;
using ECommerceAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly ISaleService _service;

        public SalesController(ISaleService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("List")]
        [SwaggerResponse(200, "Ok", typeof(BaseCollectionResponse<SaleDtoSingleResponse>))]
        public async Task<IActionResult> List([FromQuery] int page = 1, int rows = 10,
            string initialDate = "",
            string finalDate = "",
            string dni = "",
            string invoiceNumber = "")
        {
            BaseCollectionResponse<SaleDtoSingleResponse> response;

            if (!string.IsNullOrEmpty(initialDate) && !string.IsNullOrEmpty(finalDate))
                response = await _service.SelectByDate(Convert.ToDateTime(initialDate), Convert.ToDateTime(finalDate), page, rows);
            else if (!string.IsNullOrEmpty(dni))
                response = await _service.SelectByDni(dni, page, rows);
            else
                response = await _service.SelectByInvoiceNumber(invoiceNumber, page, rows);

            return Ok(response);
        }

        [HttpPost]
        [Route("Create")]
        [SwaggerResponse(201, "Created", typeof(BaseResponse<string>))]
        [SwaggerResponse(400, "Bad Request", typeof(BaseResponse<string>))]
        public async Task<IActionResult> Create([FromBody] SaleDtoRequest request)
        {
            var response = await _service.CreateAsync(request);

            if (response.Success)
                return CreatedAtAction("GetDetail", response, response.Result);

            return BadRequest(response);
        }

        [HttpGet]
        [Route("GetDetails/{saleId}")]
        [SwaggerResponse(200, "Ok", typeof(BaseCollectionResponse<SaleDetailSingleResponse>))]
        [SwaggerResponse(404, "Not Found", typeof(BaseCollectionResponse<SaleDetailSingleResponse>))]
        public async Task<IActionResult> GetDetail(string saleId)
        {
            var response = await _service.SelectDetailsAsync(saleId);

            if (response.Success)
                return Ok(response);

            return NotFound(response);
        }

        [HttpGet]
        [Route("Report")]
        [SwaggerResponse(200, "Ok", typeof(BaseResponse<ICollection<ReportByMonthSingleDto>>))]
        [SwaggerResponse(400, "Bad Request", typeof(BaseResponse<ICollection<ReportByMonthSingleDto>>))]
        public async Task<IActionResult> ReportByMonth([FromQuery] int month, int year)
        {
            var response = await _service.ReportByMonth(month, year);
            if (response.Success)
                return Ok(response);

            return BadRequest(response);
        }
    }
}
