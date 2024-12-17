using api.DTOs.ProductOption;
using api.Models;

namespace api.Mappers
{
    public static class ProductOptionMapper
    {
        public static ProductOption ParseToProductOption(this ProductOptionCreateDTO createDTO){
            return new ProductOption{
                Name = createDTO.Name,
            };
        }
    }
}