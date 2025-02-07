using api.DTOs.Order;
using api.DTOs.Product;
using api.Models;

namespace api.Services.Interfaces
{
    public interface IOrderService
    {
        Task<Order?> Update(OrderUpdateDTO updateDTO);
        Task<Order?> Create(User user, OrderCreateDTO createDTO);
    }
}