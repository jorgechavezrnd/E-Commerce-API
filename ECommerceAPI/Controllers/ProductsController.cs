using ECommerceAPI.Dto.Request;
using ECommerceAPI.Dto.Response;
using ECommerceAPI.Entities;
using ECommerceAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace ECommerceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles = Constants.RoleMixed)]
        [SwaggerResponse(200, "Ok", typeof(ProductDtoCollectionResponse))]
        public async Task<ActionResult<ProductDtoCollectionResponse>> Get(string filter, int page = 1, int rows = 10)
        {
            var response = await _service.ListAsync(filter, page, rows);

            return Ok(response);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = Constants.RoleAdministrator)]
        [SwaggerResponse(200, "Ok", typeof(BaseResponse<ProductSingleDto>))]
        [SwaggerResponse(404, "Not Found", typeof(BaseResponse<ProductSingleDto>))]
        public async Task<ActionResult<BaseResponse<ProductSingleDto>>> GetProduct(string id)
        {
            var response = await _service.GetAsync(id);

            if (response.Result == null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = Constants.RoleAdministrator)]
        [SwaggerResponse(200, "Ok", typeof(BaseResponse<string>))]
        public async Task<IActionResult> PutProduct(string id, [FromBody] ProductDtoRequest request)
        {
            var response = await _service.UpdateAsync(id, request);

            return Ok(response);
        }

        [HttpPost]
        [Authorize(Roles = Constants.RoleAdministrator)]
        [SwaggerResponse(201, "Created", typeof(BaseResponse<string>))]
        public async Task<ActionResult<BaseResponse<string>>> PostProduct([FromBody] ProductDtoRequest request)
        {
            var response = await _service.CreateAsync(request);

            return CreatedAtAction("GetProduct", new { id = response.Result }, response);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Constants.RoleAdministrator)]
        [SwaggerResponse(200, "Ok", typeof(BaseResponse<string>))]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var response = await _service.DeleteAsync(id);

            return Ok(response);
        }
    }
}
