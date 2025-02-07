using api.DTOs.CartItem;
using api.Models;

namespace api.Mappers
{
    public static class CartItemMapper
    {
        public static CartItem ParseToCartItem(this CartItemCreateDTO createDTO, Guid cartId)
        {
            if (createDTO.Anonymous)
            {
                return new CartItem
                {
                    IsSelected = createDTO.IsSelected,
                    Quantity = createDTO.Quantity,
                    OrderId = createDTO.OrderId,
                    ProductId = createDTO.ProductId,
                    ProductOptionId = createDTO.ProductOptionId
                };
            }
            return new CartItem
            {
                IsSelected = createDTO.IsSelected,
                Quantity = createDTO.Quantity,
                OrderId = createDTO.OrderId,
                ProductId = createDTO.ProductId,
                CartId = cartId,
                ProductOptionId = createDTO.ProductOptionId
            };
        }
    }
}