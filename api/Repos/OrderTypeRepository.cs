using api.Datas;
using api.Models;
using api.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Repos
{
    public class OrderTypeRepository(ApplicationContext context) : IRepository<OrderType>
    {
        private readonly ApplicationContext _context = context;
        public async Task<OrderType?> Create(OrderType target)
        {
            await _context.OrderTypes.AddAsync(target);
            await _context.SaveChangesAsync();
            return target;
        }

        public async Task<OrderType?> Delete(Guid id)
        {
            var existOrderType = await _context.OrderTypes.FirstOrDefaultAsync(x => x.Id == id);
            if (existOrderType == null) return null;
            _context.OrderTypes.Remove(existOrderType);
            await _context.SaveChangesAsync();
            return existOrderType;
        }

        public async Task<List<OrderType>> FindAll()
        {
            return await _context.OrderTypes.ToListAsync();
        }

        public async Task<OrderType?> FindById(Guid id)
        {
            var existOrderType = await _context.OrderTypes.FirstOrDefaultAsync(x => x.Id == id);
            if (existOrderType == null) return null;
            return existOrderType;
        }

        public async Task<OrderType?> Update(Guid id, OrderType data)
        {
            _context.OrderTypes.Update(data);
            await _context.SaveChangesAsync();
            return data;
        }
    }
}