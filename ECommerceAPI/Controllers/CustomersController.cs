using ECommerceAPI.Dto.Request;
using ECommerceAPI.Dto.Response;
using ECommerceAPI.Filters;
using ECommerceAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace ECommerceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [TypeFilter(typeof(MitoCodeFilterException))]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _service;

        public CustomersController(ICustomerService service)
        {
            _service = service;
        }

        [HttpGet]
        [SwaggerResponse(200, "Ok", typeof(CustomerDtoCollectionResponse))]
        public async Task<ActionResult<CustomerDtoCollectionResponse>> Get(string filter, int page = 1, int rows = 10)
        {
            var response = await _service.GetCollectionAsync(filter, page, rows);

            return Ok(response);
        }

        [HttpGet("{id}")]
        [SwaggerResponse(200, "Ok", typeof(BaseResponse<CustomerDto>))]
        [SwaggerResponse(404, "Not Found", typeof(BaseResponse<CustomerDto>))]
        public async Task<ActionResult<BaseResponse<CustomerDto>>> GetCustomer(string id)
        {
            var response = await _service.GetCustomerAsync(id);

            if (response.Result == null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpPut("{id}")]
        [SwaggerResponse(200, "Ok", typeof(BaseResponse<string>))]
        public async Task<IActionResult> PutCustomer(string id, [FromBody] CustomerDtoRequest request)
        {
            var response = await _service.UpdateAsync(id, request);

            return Ok(response);
        }

        [HttpPost]
        [SwaggerResponse(201, "Created", typeof(BaseResponse<string>))]
        public async Task<ActionResult<BaseResponse<string>>> PostCustomer([FromBody] CustomerDtoRequest request)
        {
            var response = await _service.CreateAsync(request);

            return CreatedAtAction("GetCustomer", new { id = response.Result }, response);
        }

        [HttpDelete("{id}")]
        [SwaggerResponse(200, "Ok", typeof(BaseResponse<string>))]
        public async Task<IActionResult> DeleteCustomer(string id)
        {
            var response = await _service.DeleteAsync(id);

            return Ok(response);
        }
    }
}
