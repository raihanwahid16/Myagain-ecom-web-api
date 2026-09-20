using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using asp_net_ecommerce_web_api.DTOs;
using asp_net_ecommerce_web_api.Models;
using asp_net_ecommerce_web_api.Interfaces;
using AutoMapper;

namespace asp_net_ecommerce_web_api.Services
{
    public class CategoryService : ICategoryService
    {
        private static readonly List<Category> sv_categories = new List<Category>();
        private readonly IMapper _mapper;

        public CategoryService(IMapper mapper)
        {
            _mapper = mapper;
        }


        





        public List<CategoryReadDto> GetAllCategories()
        {
            /*
            return sv_categories.Select(c => new CategoryReadDto
            {
                CategoryId = c.CategoryId,
                Name = c.Name,
                Description = c.Description,
                CreatedAt = c.CreatedAt
            }).ToList();
            */
            return _mapper.Map<List<CategoryReadDto>>(sv_categories);
        }








        public CategoryReadDto sc_CreateCategory(CategoryCreateDto categorydata)
        {
            /*
            var newCategory = new Category
            {
                CategoryId = Guid.NewGuid(),
                Name = categorydata.Name,
                Description = categorydata.Description,
                CreatedAt = DateTime.UtcNow,
            };
            */
            var newCategory = _mapper.Map<Category>(categorydata);
            newCategory.CategoryId = Guid.NewGuid();
            newCategory.Description = categorydata.Description;
            sv_categories.Add(newCategory);

            /*
            return new CategoryReadDto
            {
                CategoryId = newCategory.CategoryId,
                Name = newCategory.Name,
                Description = newCategory.Description,
                CreatedAt = newCategory.CreatedAt,
            };
            */
            return _mapper.Map<CategoryReadDto>(newCategory);
        }








        public CategoryReadDto? sc_GetCategoryById(Guid categoryId)
        {
            var foundCategory = sv_categories.FirstOrDefault(c => c.CategoryId == categoryId);
            /*
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
            */
            return foundCategory == null ?null : _mapper.Map<CategoryReadDto>(foundCategory);


        }








        public CategoryReadDto? sc_UpdateCategoryById(Guid categoryId, CategoryUpdateDto categoryData)
        {
            var foundCategory = sv_categories.FirstOrDefault(category => category.CategoryId == categoryId);
            if (foundCategory == null)
            {
                return null;
            }
            /*
            foundCategory.Name = categoryData.Name;
            foundCategory.Description = categoryData.Description;
            */
            _mapper.Map(categoryData,foundCategory);
            /*
            return new CategoryReadDto
            {
                CategoryId = foundCategory.CategoryId,
                Name = foundCategory.Name,
                Description = foundCategory.Description,
                CreatedAt = foundCategory.CreatedAt,
            };
            */
            return _mapper.Map<CategoryReadDto>(foundCategory);
        }








        public bool sc_DeleteCategoryById(Guid categoryId)
        {
            var foundcategory = sv_categories.FirstOrDefault(category => category.CategoryId == categoryId);
            if (foundcategory == null)
            {
                return false;
            }
            if (foundcategory != null) sv_categories.Remove(foundcategory);
            return true;
        }








    }
}