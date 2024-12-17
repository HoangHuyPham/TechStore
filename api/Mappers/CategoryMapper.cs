using api.DTOs.Product;
using api.Models;

namespace api.Mappers
{
    public static class CategoryMapper
    {
        public static Category ParseToCategory(this CategoryDTO categoryDTO){
            return new Category{
                Id = categoryDTO.Id,
                Name = categoryDTO.Name
            };
        }
        public static Category ParseToCategory(this CategoryCreateDTO categoryDTO){
            return new Category{
                Name = categoryDTO.Name
            };
        }

        public static CategoryDTO ParseToDTO(this Category category){
            return new CategoryDTO{
                Id = category.Id,
                Name = category.Name,
            };
        }

    }
}