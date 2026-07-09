using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using asp_net_ecommerce_web_api.Models; // 🟢 সঠিক নেমস্পেস লিংক করা হয়েছে

namespace asp_net_ecommerce_web_api.Controllers
{




    
    [ApiController]
    [Route("api/categories/")]








    public class CategoryController : ControllerBase
    {
        private static List<Category> categories = new List<Category>();
        



        // akane return korar somy Results dite hobe karon amra IActionResult use korchi.
        



        // GET: api/category
        [HttpGet]
        public IActionResult GetCategories([FromQuery] string searchValue = "")
        {
            if (!string.IsNullOrEmpty(searchValue))
            {
                var searchedCategories = categories
                    .Where(c => c.Name.Contains(searchValue, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                return Ok(searchedCategories);
            }

            return Ok(categories);
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
        public IActionResult CreateCategory([FromBody] Category categoryData)
        {
            if (string.IsNullOrEmpty(categoryData.Name))
           {
               return BadRequest("Category name is required and cannot be empty");
           }
           var newCategory = new Category
          {
          CategoryId = Guid.NewGuid(),
          Name = categoryData.Name,
          Description = categoryData.Description,
          CreatedAt = DateTime.UtcNow,
          };
          categories.Add(newCategory);
               return Created($"/api/categories/{newCategory.CategoryId}", newCategory);
     }









        // PUT: api/category/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateCategory(Guid id, [FromBody] Category updatedData)
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