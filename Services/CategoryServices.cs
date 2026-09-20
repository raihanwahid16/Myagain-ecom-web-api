using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using asp_net_ecommerce_web_api.DTOs;
using asp_net_ecommerce_web_api.Models;
using asp_net_ecommerce_web_api.Interfaces;

namespace asp_net_ecommerce_web_api.Services
{
    public class CategoryService:ICategoryService
    {
        private static readonly List<Category> sv_categories = new List<Category>();








        public List<CategoryReadDto> GetAllCategories()
        {
            return sv_categories.Select(c => new CategoryReadDto
            {
                CategoryId = c.CategoryId,
                Name = c.Name,
                Description = c.Description,
                CreatedAt = c.CreatedAt
            }).ToList();
        }








        public CategoryReadDto sc_CreateCategory(CategoryCreateDto categorydata)
        {
            var newCategory = new Category
            {
                CategoryId = Guid.NewGuid(),
                Name = categorydata.Name,
                Description = categorydata.Description,
                CreatedAt = DateTime.UtcNow,
            };

            sv_categories.Add(newCategory);

            return new CategoryReadDto
            {
                CategoryId = newCategory.CategoryId,
                Name = newCategory.Name,
                Description = newCategory.Description,
                CreatedAt = newCategory.CreatedAt,
            };
        }








        public CategoryReadDto? sc_GetCategoryById(Guid categoryId)
        {
            var foundCategory = sv_categories.FirstOrDefault(c => c.CategoryId == categoryId);

            if (foundCategory == null)
            {
                return null;
            }
            return new CategoryReadDto
            {
                CategoryId = foundCategory.CategoryId,
                Name = foundCategory.Name,
                Description = foundCategory.Description,
                CreatedAt = foundCategory.CreatedAt
            };

        }








        public CategoryReadDto? sc_UpdateCategoryById(Guid categoryId, CategoryUpdateDto categoryData)
        {
            var foundCategory = sv_categories.FirstOrDefault(category => category.CategoryId == categoryId);
            if (foundCategory == null)
            {
                return null;
            }

            foundCategory.Name = categoryData.Name;
            foundCategory.Description = categoryData.Description;

            return new CategoryReadDto
            {
                CategoryId = foundCategory.CategoryId,
                Name = foundCategory.Name,
                Description = foundCategory.Description,
                CreatedAt = foundCategory.CreatedAt,
            };
        }








        public bool sc_DeleteCategoryById(Guid categoryId)
        {
            var foundcategory = sv_categories.FirstOrDefault(category => category.CategoryId == categoryId);
            if(foundcategory == null)
            {
                return false;
            }
            if (foundcategory != null) sv_categories.Remove(foundcategory);
            return true;
        }








    }
}