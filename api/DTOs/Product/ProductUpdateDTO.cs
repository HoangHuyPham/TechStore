using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Product
{
    public class ProductUpdateDTO
    {
        public required Guid Id { get; set; }
        [StringLength(256)]
        public required string Name { get; set; }
        [Url]
        public string? Thumbnail { get; set; }
        public Guid CategoryId { get; set; }
        public required ProductDetailDTO ProductDetail { get; set; }
    }
}