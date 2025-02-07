using api.Models;

namespace api.Repos.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<ICollection<Order>?> FindByBuyerId(Guid id);
    }
}