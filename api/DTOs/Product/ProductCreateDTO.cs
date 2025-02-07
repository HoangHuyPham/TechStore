using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Product
{
    public class ProductCreateDTO
    {
        [StringLength(256)]
        public required string Name { get; set; }
        public Guid? ThumbnailId { get; set; }
        public Guid CategoryId { get; set; }
        public required ProductDetailCreateDTO ProductDetail { get; set; }
    }
}