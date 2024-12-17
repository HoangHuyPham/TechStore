using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Product
{
    public class ProductCreateDTO
    {
        [StringLength(256)]
        public required string Name { get; set; }
        [Url]
        public string? Thumbnail { get; set; }
        public Guid CategoryId { get; set; }
        public required ProductDetailCreateDTO ProductDetail { get; set; }
    }
}