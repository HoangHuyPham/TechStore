using api.Datas;
using api.DTOs;
using api.Models;
using api.Query;
using api.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Repos
{
    public class ProductRepository(ApplicationContext context) : IProductRepository
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
            .Include(e => e.Category)
            .Include(x => x.Thumbnail)
            .Include(e => e.ProductDetail).ThenInclude(e => e.ProductOptions)
            .Include(e => e.ProductDetail).ThenInclude(e => e.Previews)
            .ToListAsync();
        }

        public async Task<PaginationResponseDTO<Product>> FindAll(ProductQuery query)
        {
            var products = _context.Products.AsQueryable();

            // Join
            products = products.Include(e => e.Category)
            .Include(x => x.Thumbnail)
                    .Include(e => e.ProductDetail).ThenInclude(e => e.ProductOptions)
                    .Include(e => e.ProductDetail).ThenInclude(e => e.Previews);

            // Price filter
            products = products.Where(e => e.ProductDetail.Price >= query.MinPrice && e.ProductDetail.Price <= query.MaxPrice);

            // Category filter
            if (query.CategoryId != null)
            {
                products = products.Where(e => e.Category != null && e.Category.Id == query.CategoryId);
            }

            // Keyword filter 
            if (query.KeyWord != null)
            {
                products = products.Where(e => e.Name.Contains(query.KeyWord));
            }

            int ItemCount = await products.CountAsync();
            int TotalPage = (int)Math.Ceiling((double)ItemCount / query.PageSize);
            int CurrentPage = query.Page > TotalPage ? TotalPage : query.Page;

            // Pagination
            products = products.Skip((CurrentPage - 1) * query.PageSize).Take(query.PageSize);

            return new PaginationResponseDTO<Product>
            {
                Items = await products.ToListAsync(),
                CurrentPage = TotalPage,
                PageSize = query.PageSize,
                TotalItems = ItemCount,
                TotalPage = TotalPage
            };
        }

        public async Task<Product?> FindById(Guid id)
        {
            var existProduct = await _context.Products
            .Include(x => x.Thumbnail)
            .Include(e => e.ProductDetail).ThenInclude(e => e.ProductOptions)
            .Include(e => e.ProductDetail).ThenInclude(e => e.Previews)
            .FirstOrDefaultAsync(x => x.Id == id);
            if (existProduct == null) return null;
            return existProduct;
        }

        public async Task<Product?> Update(Guid id, Product data)
        {
            _context.Products.Update(data);
            await _context.SaveChangesAsync();
            return data;
        }
    }
}