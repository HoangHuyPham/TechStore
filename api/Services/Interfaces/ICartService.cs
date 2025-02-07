using api.Models;

namespace api.Services.Interfaces
{
    public interface ICartService
    {
        Task<Cart?> AddCartItemToCart(Guid cartId, CartItem item);
    }
}