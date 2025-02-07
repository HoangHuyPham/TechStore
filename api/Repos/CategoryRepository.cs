using api.Datas;
using api.Models;
using api.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Repos
{
    public class CategoryRepository(ApplicationContext context) : IRepository<Category>
    {
        private readonly ApplicationContext _context = context;
        public async Task<Category?> Create(Category target)
        {
            await _context.Categories.AddAsync(target);
            await _context.SaveChangesAsync();
            return target;
        }

        public async Task<Category?> Delete(Guid id)
        {
            var existCategory = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (existCategory == null) return null;
            _context.Categories.Remove(existCategory);
            await _context.SaveChangesAsync();
            return existCategory;
        }

        public async Task<List<Category>> FindAll()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category?> FindById(Guid id)
        {
            var existCategory = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (existCategory == null) return null;
            return existCategory;
        }

        public async Task<Category?> Update(Guid id, Category data)
        {
            _context.Categories.Update(data);
            await _context.SaveChangesAsync();
            return data;
        }
    }
}