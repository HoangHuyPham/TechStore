using api.Datas;
using api.Models;
using api.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Repos
{
    public class CartItemRepository(ApplicationContext context) : IRepository<CartItem>
    {
        private readonly ApplicationContext _context = context;
        public async Task<CartItem?> Create(CartItem target)
        {
            await _context.CartItems.AddAsync(target);
            await _context.SaveChangesAsync();
            return target;
        }

        public async Task<CartItem?> Delete(Guid id)
        {
            var existCartItem = await _context.CartItems.FirstOrDefaultAsync(x => x.Id == id);
            if (existCartItem == null) return null;
            _context.CartItems.Remove(existCartItem);
            await _context.SaveChangesAsync();
            return existCartItem;
        }

        public async Task<List<CartItem>> FindAll()
        {
            return await _context.CartItems.ToListAsync();
        }

        public async Task<CartItem?> FindById(Guid id)
        {
            var existCartItem = await _context.CartItems.FirstOrDefaultAsync(x => x.Id == id);
            if (existCartItem == null) return null;
            return existCartItem;
        }

        public async Task<CartItem?> Update(Guid id, CartItem data)
        {
            _context.CartItems.Update(data);
            await _context.SaveChangesAsync();
            return data;
        }
    }
}