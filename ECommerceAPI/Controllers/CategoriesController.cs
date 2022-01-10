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
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoriesController(ICategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles = Constants.RoleMixed)]
        [SwaggerResponse(200, "Ok", typeof(CategoryDtoCollectionResponse))]
        public async Task<IActionResult> GetCategories(string filter, int page = 1, int rows = 10)
        {
            return Ok(await _service.ListAsync(filter, page, rows));
        }

        [HttpGet]
        [Authorize(Roles = Constants.RoleAdministrator)]
        [Route("{id}")]
        [SwaggerResponse(200, "Ok", typeof(BaseResponse<CategoryDto>))]
        [SwaggerResponse(404, "Not Found", typeof(BaseResponse<CategoryDto>))]
        public async Task<IActionResult> GetCategories(string id)
        {
            var response = await _service.GetAsync(id);

            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpPost]
        [Authorize(Roles = Constants.RoleAdministrator)]
        [SwaggerResponse(201, "Ok", typeof(BaseResponse<string>))]
        public async Task<IActionResult> PostCategories([FromBody] CategoryRequest request)
        {
            var response = await _service.CreateAsync(request);

            return CreatedAtAction("GetCategories", new
            {
                id = response.Result
            }, response);
        }

        [HttpPut]
        [Authorize(Roles = Constants.RoleAdministrator)]
        [Route("{id}")]
        [SwaggerResponse(200, "Ok", typeof(BaseResponse<string>))]
        public async Task<IActionResult> PutCategories(string id, [FromBody] CategoryRequest request)
        {
            return Ok(await _service.UpdateAsync(id, request));
        }

        [HttpDelete]
        [Authorize(Roles = Constants.RoleAdministrator)]
        [Route("{id}")]
        [SwaggerResponse(200, "Ok", typeof(BaseResponse<string>))]
        public async Task<IActionResult> DeleteCategories(string id)
        {
            return Ok(await _service.DeleteAsync(id));
        }
    }
}
