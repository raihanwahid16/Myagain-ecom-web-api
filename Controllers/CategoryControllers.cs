using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using asp_net_ecommerce_web_api.Models; // 🟢 সঠিক নেমস্পেস লিংক করা হয়েছে
using asp_net_ecommerce_web_api.DTOs;
using asp_net_ecommerce_web_api.Services;
using asp_net_ecommerce_web_api.Interfaces;



namespace asp_net_ecommerce_web_api.Controllers
{








    [ApiController]
    [Route("api/categories/")]








    public class CategoryController : ControllerBase
    {








        private ICategoryService sv_categoryService;
        public CategoryController(ICategoryService ps_category_service)
        {
            sv_categoryService = ps_category_service;
        }








        // GET: api/category
        [HttpGet]
        public IActionResult GetCategories([FromQuery] string searchValue = "")
        {
            var categoryList = sv_categoryService.GetAllCategories();
            return Ok(ApiResponse<List<CategoryReadDto>>.SuccessResponse(categoryList, 200,
            "Catgeories returned successfully"));
        }








        // POST: api/category
        [HttpPost]
        public IActionResult fi_CreateCategory([FromBody] CategoryCreateDto categorydata)
        {
            var sv_categoryread = sv_categoryService.sc_CreateCategory(categorydata);
            return Created($"/api/categories/{sv_categoryread.CategoryId}",
            ApiResponse<CategoryReadDto>.SuccessResponse(sv_categoryread, 201, "Catgeory created successfully"));
        }








        // GET: api/category/{id}
        //[HttpGet("{id}")]
        [HttpGet("{category_id:guid}")]
        public IActionResult fi_GetCategoryById(Guid category_id)
        {
            var category = sv_categoryService.sc_GetCategoryById(category_id);

            if (category == null)
            {
                return NotFound(ApiResponse<object>.ErrorResponse(new List<string>
               { "Category with this ID does not exist" }, 404, "Validation failed"));
            }

            return Ok(ApiResponse<CategoryReadDto>.SuccessResponse(category, 200, "Catgeory is returned successfully"));
        }








        // PUT: /api/categories/{categoryId} => Update a category
        [HttpPut("{categoryId:guid}")]
        public IActionResult fi_UpdateCategoryById(Guid categoryId, [FromBody] CategoryUpdateDto categoryData)
        {
            var updateCategory = sv_categoryService.sc_UpdateCategoryById(categoryId, categoryData);
            if (updateCategory == null)
            {
                return NotFound(ApiResponse<object>.ErrorResponse(new List<string>
        { "Category with this ID does not exist" }, 404, "Validation failed"));
            }

            return Ok(ApiResponse<CategoryReadDto>.SuccessResponse(updateCategory, 200, "Catgeory Updated successfully"));
        }








        // DELETE: api/category/{id}
        //[HttpDelete("{id}")]
        [HttpDelete("{category_id:guid}")]
        public IActionResult fi_DeleteCategory(Guid category_id)
        {
            var ck_foundcategory = sv_categoryService.sc_DeleteCategoryById(category_id);
            if (!ck_foundcategory)
            {
                return NotFound(ApiResponse<object>.ErrorResponse(new List<string>
                { "Category with this ID does not exist" }, 404, "Validation failed"));
            }

            return Ok(ApiResponse<object>.SuccessResponse(null, 204, "Category deleted successfully"));
        }








    }
}