namespace api.DTOs.ProductOption
{
    public class ProductOptionDTO
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public Guid? ProductDetailId { get; set; }
    }
}