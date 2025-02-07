namespace api.DTOs.CartItem
{
    public class CartItemUpdateDTO
    {
        public short Quantity { get; set; } = 1;
        public bool IsSelected { get; set; }
        public Guid? ProductOptionId { get; set; }
    }
}