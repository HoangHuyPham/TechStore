namespace api.DTOs.Preview
{
    public class PreviewDTO
    {
        public Guid Id { get; set; }
        public required string URL { get; set; }
        public Guid? ProductDetailId { get; set; }
    }
}