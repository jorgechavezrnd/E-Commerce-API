using ECommerceAPI.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ECommerceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly List<Category> _categories;

        public CategoriesController(List<Category> categories)
        {
            _categories = categories;
        }

        [HttpGet]
        public IActionResult GetCategories()
        {
            return Ok(_categories);
        }

        [HttpPost]
        public IActionResult PostCategories([FromBody] CategoryRequest request)
        {
            var category = new Category
            {
                Id = Guid.NewGuid().ToString(),
                Name = request.Name,
                Description = request.Description
            };

            _categories.Add(category);
            return Ok(new
            {
                Id = category.Id
            });
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult PutCategories(string id, [FromBody] CategoryRequest request)
        {
            var find = _categories.FirstOrDefault(p => p.Id == id);

            if (find == null)
            {
                return NotFound(new
                {
                    id
                });
            }

            find.Description = request.Description;
            find.Name = request.Name;

            return Ok(new
            {
                id
            });
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteCategories(string id)
        {
            var find = _categories.FirstOrDefault(p => p.Id == id);

            if (find == null)
            {
                return NotFound(new
                {
                    id
                });
            }

            _categories.Remove(find);

            return Ok(new
            {
                id
            });
        }

    }
}
