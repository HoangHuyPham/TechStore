using api.Datas;
using api.Models;
using api.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Repos
{
    public class OrderRepository(ApplicationContext context) : IOrderRepository
    {
        private readonly ApplicationContext _context = context;
        public async Task<Order?> Create(Order target)
        {
            await _context.Orders.AddAsync(target);
            await _context.SaveChangesAsync();
            return target;
        }

        public async Task<Order?> Delete(Guid id)
        {
            var existOrder = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);
            if (existOrder == null) return null;
            _context.Orders.Remove(existOrder);
            await _context.SaveChangesAsync();
            return existOrder;
        }

        public async Task<List<Order>> FindAll()
        {
            return await _context.Orders.Include(x=>x.CartItems!).ThenInclude(x=>x.Product!).ThenInclude(x=>x.ProductDetail).Include(x=>x.Voucher).Include(x=>x.OrderType).ToListAsync();
        }

        public async Task<ICollection<Order>?> FindByBuyerId(Guid id)
        {
            var existOrders = await _context.Orders.Include(x=>x.CartItems!).ThenInclude(x=>x.Product!).ThenInclude(x=>x.ProductDetail).Include(x=>x.Voucher).Include(x=>x.OrderType).Where(x=>x.BuyerId == id).ToListAsync();
            if (existOrders == null) return null;
            return existOrders;
        }

        public async Task<Order?> FindById(Guid id)
        {
            var existOrder = await _context.Orders.Include(x=>x.CartItems!).ThenInclude(x=>x.Product!).ThenInclude(x=>x.ProductDetail).Include(x=>x.Voucher).Include(x=>x.OrderType).FirstOrDefaultAsync(x => x.Id == id);
            if (existOrder == null) return null;
            return existOrder;
        }

        public async Task<Order?> Update(Guid id, Order data)
        {
            _context.Orders.Update(data);
            await _context.SaveChangesAsync();
            return data;
        }
    }
}