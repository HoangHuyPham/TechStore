using api.DTOs.Preview;
using api.DTOs.Product;
using api.DTOs.ProductOption;
using api.Models;

namespace api.Mappers
{
    public static class ProductDetailMapper
    {
        public static ProductDetailDTO ParseToDTO(this ProductDetail product)
        {
            return new ProductDetailDTO
            {
                Id = product.Id,
                Description = product.Description,
                BeforePrice = product.BeforePrice,
                Price = product.Price,
                Stock = product.Stock,
                TotalRating = product.TotalRating,
                Previews = product?.Previews?.Select(x => new PreviewDTO
                {
                    Id = x.Id,
                    URL = x.URL,
                    ProductDetailId = x.ProductDetailId,
                }).ToList(),
                ProductOptions = product?.ProductOptions?.Select(x => new ProductOptionDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    ProductDetailId = x.ProductDetailId
                }).ToList(),
            };
        }

        public static ProductDetail ParseToProductDetail(this ProductDetailDTO productDetailDTO)
        {
            return new ProductDetail
            {
                Id = productDetailDTO.Id,
                Description = productDetailDTO.Description,
                BeforePrice = productDetailDTO.BeforePrice,
                Price = productDetailDTO.Price,
                Stock = productDetailDTO.Stock,
                TotalRating = productDetailDTO.TotalRating,
                Previews = productDetailDTO?.Previews?.Select(x => new Preview
                {
                    Id = x.Id,
                    URL = x.URL,
                    ProductDetailId = x.ProductDetailId,
                }).ToList(),
                ProductOptions = productDetailDTO?.ProductOptions?.Select(x => new ProductOption
                {
                    Id = x.Id,
                    Name = x.Name,
                    ProductDetailId = x.ProductDetailId
                }).ToList(),
            };
        }
    

    public static ProductDetail ParseToProductDetail(this ProductDetailCreateDTO createDTO)
        {
            return new ProductDetail
            {
                Description = createDTO.Description,
                BeforePrice = createDTO.BeforePrice,
                Price = createDTO.Price,
                Stock = createDTO.Stock,
                TotalRating = createDTO.TotalRating,
                Previews = createDTO?.Previews?.Select(x => x.ParseToPreview()).ToList(),
                ProductOptions = createDTO?.ProductOptions?.Select(x => x.ParseToProductOption()).ToList(),
            };
        }
    }
}