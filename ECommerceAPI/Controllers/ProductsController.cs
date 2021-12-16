using ECommerceAPI.Dto.Request;
using ECommerceAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> GetProducts(string filter, int page = 1, int rows = 10)
        {
            return Ok(await _service.ListAsync(filter, page, rows));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetProducts(string id)
        {
            var response = await _service.GetAsync(id);

            if (!response.Success)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> PostProducts([FromBody] ProductRequest request)
        {
            var response = await _service.CreateAsync(request);

            return CreatedAtAction("GetProducts", new
            {
                id = response.Result
            }, response);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> PutProducts(string id, [FromBody] ProductRequest request)
        {
            return Ok(await _service.UpdateAsync(id, request));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteProducts(string id)
        {
            return Ok(await _service.DeleteAsync(id));
        }
    }
}
