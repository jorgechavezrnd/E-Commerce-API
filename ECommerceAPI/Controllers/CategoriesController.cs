using ECommerceAPI.DataAccess;
using ECommerceAPI.Dto.Request;
using ECommerceAPI.Dto.Response;
using ECommerceAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace ECommerceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ECommerceDbContext _dbContext;

        public CategoriesController(ECommerceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetCategories()
        {
            var response = new CategoryDtoCollectionResponse();

            try
            {
                response.Collection = _dbContext.Categories
                    .Where(p => p.Status)
                    .Select(p => new CategoryDto
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Description = p.Description
                    })
                    .ToList();

                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
            }

            return Ok(response);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetCategories(string id)
        {
            var response = new BaseResponse<CategoryDto>();

            var find = _dbContext.Categories.FirstOrDefault(p => p.Id == id && p.Status);

            if (find == null)
            {
                return NotFound(response);
            }

            response.Success = true;
            response.Result = new CategoryDto
            {
                Id = find.Id,
                Name = find.Name,
                Description = find.Description
            };

            return Ok(response);
        }

        [HttpPost]
        public IActionResult PostCategories([FromBody] CategoryRequest request)
        {
            var response = new BaseResponse<string>();

            var category = new Category
            {
                Name = request.Name,
                Description = request.Description
            };

            _dbContext.Categories.Add(category);
            _dbContext.SaveChanges();

            response.Success = true;
            response.Result = category.Id;

            return CreatedAtAction("GetCategories", new
            {
                id = response.Result
            }, response);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult PutCategories(string id, [FromBody] CategoryRequest request)
        {
            var response = new BaseResponse<string>();
            var find = _dbContext.Categories.FirstOrDefault(p => p.Id == id);

            if (find == null)
            {
                return NotFound(response);
            }

            find.Description = request.Description;
            find.Name = request.Name;

            _dbContext.Categories.Attach(find);
            _dbContext.Entry(find).State = EntityState.Modified;
            _dbContext.SaveChanges();

            response.Success = true;
            response.Result = id;

            return Ok(response);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteCategories(string id)
        {
            var response = new BaseResponse<string>();
            var find = _dbContext.Categories.FirstOrDefault(p => p.Id == id);

            if (find == null)
            {
                return NotFound(response);
            }

            find.Status = false;
            _dbContext.Categories.Attach(find);
            _dbContext.Entry(find).State = EntityState.Modified;
            _dbContext.SaveChanges();

            response.Success = true;
            response.Result = id;

            return Ok(response);
        }

    }
}
