using api.Datas;
using api.Models;
using api.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Repos
{
    public class RoleRepository(ApplicationContext context) : IRepository<Role>
    {
        private readonly ApplicationContext _context = context;
        public async Task<Role?> Create(Role target)
        {
            await _context.Roles.AddAsync(target);
            await _context.SaveChangesAsync();
            return target;
        }

        public async Task<Role?> Delete(Guid id)
        {
            var existRole = await _context.Roles.FirstOrDefaultAsync(x => x.Id == id);
            if (existRole == null) return null;
            _context.Roles.Remove(existRole);
            await _context.SaveChangesAsync();
            return existRole;
        }

        public async Task<List<Role>> FindAll()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Role?> FindById(Guid id)
        {
            var existRole = await _context.Roles.FirstOrDefaultAsync(x => x.Id == id);
            if (existRole == null) return null;
            return existRole;
        }

        public async Task<Role?> Update(Guid id, Role data)
        {
             _context.Roles.Update(data);
            await _context.SaveChangesAsync();
            return data;
        }
    }
}