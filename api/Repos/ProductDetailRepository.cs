using api.Datas;
using api.Models;
using api.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Repos
{
    public class ProductDetailRepository(ApplicationContext context) : IRepository<ProductDetail>
    {
        private readonly ApplicationContext _context = context;
        public async Task<ProductDetail?> Create(ProductDetail target)
        {
            await _context.ProductDetails.AddAsync(target);
            await _context.SaveChangesAsync();
            return target;
        }

        public async Task<ProductDetail?> Delete(Guid id)
        {
            var existProductDetail = await _context.ProductDetails.FirstOrDefaultAsync(x => x.Id == id);
            if (existProductDetail == null) return null;
            _context.ProductDetails.Remove(existProductDetail);
            await _context.SaveChangesAsync();
            return existProductDetail;
        }

        public async Task<List<ProductDetail>> FindAll()
        {
            return await _context.ProductDetails.ToListAsync();
        }

        public async Task<ProductDetail?> FindById(Guid id)
        {
            var existProductDetail = await _context.ProductDetails.FirstOrDefaultAsync(x => x.Id == id);
            if (existProductDetail == null) return null;
            return existProductDetail;
        }

        public async Task<ProductDetail?> Update(Guid id, ProductDetail data)
        {
            var existProductDetail = await _context.ProductDetails.FirstOrDefaultAsync(x => x.Id == id);
            if (existProductDetail == null) return null;
            existProductDetail.Description = data.Description;
            existProductDetail.BeforePrice = data.BeforePrice;
            existProductDetail.Price = data.Price;
            existProductDetail.Stock = data.Stock;
            existProductDetail.TotalRating = data.TotalRating;
            await _context.SaveChangesAsync();
            return existProductDetail;
        }
    }
}