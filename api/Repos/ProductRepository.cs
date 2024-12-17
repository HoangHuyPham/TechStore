using api.Datas;
using api.Models;
using api.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Repos
{
    public class ProductRepository(ApplicationContext context) : IRepository<Product>
    {
        private readonly ApplicationContext _context = context;
        public async Task<Product?> Create(Product target)
        {
            await _context.Products.AddAsync(target);
            await _context.SaveChangesAsync();
            return target;
        }

        public async Task<Product?> Delete(Guid id)
        {
            var existProduct = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (existProduct == null) return null;
            _context.Products.Remove(existProduct);
            await _context.SaveChangesAsync();
            return existProduct;
        }

        public async Task<List<Product>> FindAll()
        {
            return await _context.Products
            .Include(e=>e.Category)
            .Include(e=>e.ProductDetail).ThenInclude(e=>e.ProductOptions)
            .Include(e=>e.ProductDetail).ThenInclude(e=>e.Previews)
            .ToListAsync();
        }

        public async Task<Product?> FindById(Guid id)
        {
            var existProduct = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (existProduct == null) return null;
            return existProduct;
        }

        public async Task<Product?> Update(Guid id, Product data)
        {
            var existProduct = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (existProduct == null) return null;
            existProduct.Name = data.Name;
            existProduct.Thumbnail = data.Thumbnail;
            existProduct.ProductDetail = data.ProductDetail;
            existProduct.Category = data.Category;
            await _context.SaveChangesAsync();
            return existProduct;
        }
    }
}