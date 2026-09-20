using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using asp_net_ecommerce_web_api.DTOs;

namespace asp_net_ecommerce_web_api.Interfaces
{
    public interface ICategoryService
    {
        List<CategoryReadDto> GetAllCategories();
        CategoryReadDto? sc_GetCategoryById(Guid categoryId);
        CategoryReadDto sc_CreateCategory(CategoryCreateDto categoryData);
         CategoryReadDto? sc_UpdateCategoryById(Guid categoryId, CategoryUpdateDto categoryData);
         bool sc_DeleteCategoryById(Guid categoryId);

    }
}