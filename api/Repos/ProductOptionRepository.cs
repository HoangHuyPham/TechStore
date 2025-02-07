using api.Datas;
using api.Models;
using api.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Repos
{
    public class ProductOptionRepository(ApplicationContext context) : IRepository<ProductOption>
    {
        private readonly ApplicationContext _context = context;
        public async Task<ProductOption?> Create(ProductOption target)
        {
            await _context.ProductOptions.AddAsync(target);
            await _context.SaveChangesAsync();
            return target;
        }

        public async Task<ProductOption?> Delete(Guid id)
        {
            var existProductOption = await _context.ProductOptions.FirstOrDefaultAsync(x => x.Id == id);
            if (existProductOption == null) return null;
            _context.ProductOptions.Remove(existProductOption);
            await _context.SaveChangesAsync();
            return existProductOption;
        }

        public async Task<List<ProductOption>> FindAll()
        {
            return await _context.ProductOptions.ToListAsync();
        }

        public async Task<ProductOption?> FindById(Guid id)
        {
            var existProductOption = await _context.ProductOptions.FirstOrDefaultAsync(x => x.Id == id);
            if (existProductOption == null) return null;
            return existProductOption;
        }

        public async Task<ProductOption?> Update(Guid id, ProductOption data)
        {
             _context.ProductOptions.Update(data);
            await _context.SaveChangesAsync();
            return data;
        }
    }
}