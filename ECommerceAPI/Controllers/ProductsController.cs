using ECommerceAPI.Dto.Request;
using ECommerceAPI.Dto.Response;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace ECommerceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductDtoCollectionResponse _products;

        public ProductsController(ProductDtoCollectionResponse products)
        {
            _products = products;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            _products.Success = true;
            return Ok(_products);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetProducts(string id)
        {
            var response = new BaseResponse<ProductDto>();

            var find = _products.Collection.FirstOrDefault(p => p.Id == id);

            if (find == null)
            {
                return NotFound(response);
            }

            response.Success = true;
            response.Result = find;

            return Ok(response);
        }

        [HttpPost]
        public IActionResult PostProducts([FromBody] ProductRequest request)
        {
            var response = new BaseResponse<string>();

            var product = new ProductDto
            {
                Id = Guid.NewGuid().ToString(),
                Name = request.Name,
                Description = request.Description,
                Url = request.FileName
            };

            _products.Collection.Add(product);
            response.Success = true;
            response.Result = product.Id;

            return CreatedAtAction("GetProducts", new
            {
                Id = product.Id
            }, response);
        }
    }
}
