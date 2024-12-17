using api.DTOs.Preview;
using api.DTOs.ProductOption;

namespace api.DTOs.Product
{
    public class ProductDetailCreateDTO
    {
        public string? Description { get; set; }
        public int Stock { get; set; }
        public float BeforePrice { get; set; }
        public float Price { get; set; }
        public float TotalRating { get; set; }
        public ICollection<PreviewCreateDTO>? Previews { get; set; }
        public ICollection<ProductOptionCreateDTO>? ProductOptions { get; set; }
    }
}