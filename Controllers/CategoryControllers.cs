using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using asp_net_ecommerce_web_api.Models; // 🟢 সঠিক নেমস্পেস লিংক করা হয়েছে
using asp_net_ecommerce_web_api.DTOs;

namespace asp_net_ecommerce_web_api.Controllers
{




    
    [ApiController]
    [Route("api/categories/")]








    public class CategoryController : ControllerBase
    {
        private static List<Category> categories = new List<Category>();
        






        // GET: api/category
        [HttpGet]
        public IActionResult GetCategories([FromQuery] string searchValue = "")
        {
           /*
            if (!string.IsNullOrEmpty(searchValue))
            {
                var searchedCategories = categories
                    .Where(c => c.Name.Contains(searchValue, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                return Ok(searchedCategories);
            }

            return Ok(categories);
            */
            var categoryList = categories.Select(c => new CategoryReadDto
        {
            CategoryId = c.CategoryId,
            Name = c.Name,
            //Description = c.Description,
            CreatedAt = c.CreatedAt
        }).ToList();

           return Ok(categoryList);
        }








        // GET: api/category/{id}
        [HttpGet("{id}")]
        public IActionResult GetCategoryById(Guid id)
        {
            var category = categories.FirstOrDefault(c => c.CategoryId == id);
            
            if (category == null)
            {
                return NotFound($"Category not found with ID: {id}");
            }

            return Ok(category);
        }







        // POST: api/category
        [HttpPost]
        public IActionResult CreateCategory([FromBody] CategoryCreateDto categoryData)
        {
            
            
           var newCategory = new Category
          {
          CategoryId = Guid.NewGuid(),
          Name = categoryData.Name,
          Description = categoryData.Description,
          CreatedAt = DateTime.UtcNow,
          };
          categories.Add(newCategory);

          var categoryreaddto = new CategoryReadDto
          {
              CategoryId = newCategory.CategoryId,
              Name = newCategory.Name,
              //Description = newCategory.Description,
              CreatedAt = newCategory.CreatedAt
          };
               return Created($"/api/categories/{newCategory.CategoryId}", categoryreaddto);
     }









        // PUT: api/category/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateCategory(Guid id, [FromBody] CategoryUpdateDto updatedData)
        {
            var existingCategory = categories.FirstOrDefault(c => c.CategoryId == id);

            if (existingCategory == null)
            {
                return NotFound($"Category not found with ID: {id}");
            }

            if (string.IsNullOrWhiteSpace(updatedData.Name))
            {
                return BadRequest("Category name cannot be empty!");
            }

            existingCategory.Name = updatedData.Name;
            
            if (!string.IsNullOrWhiteSpace(updatedData.Description))
            {
                existingCategory.Description = updatedData.Description;
            }

            return NoContent();
        }









        // DELETE: api/category/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(Guid id)
        {
            var category = categories.FirstOrDefault(c => c.CategoryId == id);

            if (category == null)
            {
                return NotFound($"Category not found with ID: {id}");
            }

            categories.Remove(category);
            return NoContent();
        }








    }
}