using api.DTOs.Product;
using api.Models;

namespace api.Mappers
{
    public static class ProductMapper
    {
        public static ProductDTO ParseToDTO(this Product product){
            return new ProductDTO{
                Id = product.Id,
                Name = product.Name,
                Category = product.Category?.ParseToDTO(),
                // Thumbnail = product.Thumbnail,
                ProductDetail = product.ProductDetail.ParseToDTO(),
                CreatedOn = product?.CreatedOn
            };
        }
        public static Product ParseToProduct(this ProductDTO productDTO){
            return new Product{
                Id = productDTO.Id,
                Name = productDTO.Name,
                Category = productDTO.Category?.ParseToCategory(),
                // Thumbnail = productDTO.Thumbnail,
                ProductDetail = productDTO.ProductDetail.ParseToProductDetail(),
            };
        }

        public static Product ParseToProduct(this ProductCreateDTO createDTO){
            return new Product{
                Name = createDTO.Name,
                // Thumbnail = createDTO.Thumbnail,
                ProductDetail = createDTO.ProductDetail.ParseToProductDetail(),
            };
        }
        public static Product ParseToProduct(this ProductUpdateDTO updateDTO){
            return new Product{
                Name = updateDTO.Name,
                // Thumbnail = updateDTO.Thumbnail,
                ProductDetail = updateDTO.ProductDetail.ParseToProductDetail(), 
            };
        }
    }
}