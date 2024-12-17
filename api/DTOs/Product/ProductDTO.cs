namespace api.DTOs.Product
{
    public class ProductDTO
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Thumbnail { get; set; }
        public CategoryDTO? Category { get; set; }
        public required ProductDetailDTO ProductDetail { get; set; }
        public DateTime CreatedOn { get; set;}
    }
}