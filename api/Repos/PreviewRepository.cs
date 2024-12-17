using api.Datas;
using api.Models;
using api.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Repos
{
    public class PreviewRepository(ApplicationContext context) : IRepository<Preview>
    {
        private readonly ApplicationContext _context = context;
        public async Task<Preview?> Create(Preview target)
        {
            await _context.Previews.AddAsync(target);
            await _context.SaveChangesAsync();
            return target;
        }

        public async Task<Preview?> Delete(Guid id)
        {
            var existPreview = await _context.Previews.FirstOrDefaultAsync(x => x.Id == id);
            if (existPreview == null) return null;
            _context.Previews.Remove(existPreview);
            await _context.SaveChangesAsync();
            return existPreview;
        }

        public async Task<List<Preview>> FindAll()
        {
            return await _context.Previews.ToListAsync();
        }

        public async Task<Preview?> FindById(Guid id)
        {
            var existPreview = await _context.Previews.FirstOrDefaultAsync(x => x.Id == id);
            if (existPreview == null) return null;
            return existPreview;
        }

        public async Task<Preview?> Update(Guid id, Preview data)
        {
            var existPreview = await _context.Previews.FirstOrDefaultAsync(x => x.Id == id);
            if (existPreview == null) return null;
            existPreview.URL = data.URL;
            await _context.SaveChangesAsync();
            return existPreview;
        }
    }
}