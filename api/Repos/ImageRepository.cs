using api.Datas;
using api.Models;
using api.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Repos
{
    public class ImageRepository(ApplicationContext context) : IRepository<Image>
    {
        private readonly ApplicationContext _context = context;
        public async Task<Image?> Create(Image target)
        {
            await _context.Images.AddAsync(target);
            await _context.SaveChangesAsync();
            return target;
        }

        public async Task<Image?> Delete(Guid id)
        {
            var existImage = await _context.Images.FirstOrDefaultAsync(x => x.Id == id);
            if (existImage == null) return null;
            _context.Images.Remove(existImage);
            await _context.SaveChangesAsync();
            return existImage;
        }

        public async Task<List<Image>> FindAll()
        {
            return await _context.Images.ToListAsync();
        }

        public async Task<Image?> FindById(Guid id)
        {
            var existImage = await _context.Images.FirstOrDefaultAsync(x => x.Id == id);
            if (existImage == null) return null;
            return existImage;
        }

        public async Task<Image?> Update(Guid id, Image data)
        {
            _context.Images.Update(data);
            await _context.SaveChangesAsync();
            return data;
        }
    }
}