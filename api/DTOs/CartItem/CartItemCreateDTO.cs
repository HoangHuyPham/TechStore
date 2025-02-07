namespace api.DTOs.CartItem
{
    public class CartItemCreateDTO
    {
        public required short Quantity { get; set; } = 1;
        public required bool IsSelected { get; set; }
        public Guid ProductId { get; set; }
        public Guid? ProductOptionId { get; set; }
        public Guid? OrderId { get; set; }
        public bool Anonymous { get; set; } = false;
    }
}