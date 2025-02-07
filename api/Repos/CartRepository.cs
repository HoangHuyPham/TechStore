using api.Datas;
using api.Models;
using api.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Repos
{
    public class CartRepository(ApplicationContext context) : IRepository<Cart>
    {
        private readonly ApplicationContext _context = context;
        public async Task<Cart?> Create(Cart target)
        {
            await _context.Carts.AddAsync(target);
            await _context.SaveChangesAsync();
            return target;
        }

        public async Task<Cart?> Delete(Guid id)
        {
            var existCart = await _context.Carts.FirstOrDefaultAsync(x => x.Id == id);
            if (existCart == null) return null;
            _context.Carts.Remove(existCart);
            await _context.SaveChangesAsync();
            return existCart;
        }

        public async Task<List<Cart>> FindAll()
        {
            return await _context.Carts
            .Include(x => x.CartItems!).ThenInclude(x => x.Product!).ThenInclude(x => x.ProductDetail).ThenInclude(x => x.ProductOptions)
            .Include(x => x.CartItems!).ThenInclude(x => x.Product!).ThenInclude(x => x.Thumbnail).ToListAsync();
        }

        public async Task<Cart?> FindById(Guid id)
        {
            var existCart = await _context.Carts
            .Include(x => x.CartItems!)
            .ThenInclude(x => x.Product!).ThenInclude(x => x.ProductDetail).ThenInclude(x => x.ProductOptions)
            .Include(x => x.CartItems!)
            .ThenInclude(x => x.Product!).ThenInclude(x => x.Thumbnail)
            .FirstOrDefaultAsync(x => x.Id == id);
            if (existCart == null) return null;
            return existCart;
        }

        public Task<Cart?> Update(Guid id, Cart data)
        {
            throw new NotImplementedException();
        }
    }
}