using api.Datas;
using api.Models;
using api.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Repos
{
    public class UserRepository(ApplicationContext context) : IUserRepository
    {
        private readonly ApplicationContext _context = context;
        public async Task<User?> Create(User target)
        {
            await _context.Users.AddAsync(target);
            await _context.SaveChangesAsync();
            return target;
        }

        public async Task<User?> Delete(Guid id)
        {
            var existUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (existUser == null) return null;
            _context.Users.Remove(existUser);
            await _context.SaveChangesAsync();
            return existUser;
        }

        public async Task<List<User>> FindAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> FindById(Guid id)
        {
            var existUser = await _context.Users.Include(x=>x.Role).FirstOrDefaultAsync(x => x.Id == id);
            if (existUser == null) return null;
            return existUser;
        }

        public async Task<User?> FindByEmail(string email)
        {
            var existUser = await _context.Users.Include(x=>x.Role).FirstOrDefaultAsync(x => x.Email == email);
            if (existUser == null) return null;
            return existUser;
        }

        public async Task<User?> Update(Guid id, User data)
        {
            var existUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (existUser == null) return null;
            existUser.Name = data.Name;
            existUser.Address = data.Address;
            existUser.Email = data.Email;
            existUser.Phone = data.Phone;
            existUser.Password = data.Name;
            existUser.Avatar = data.Avatar;
            existUser.Role = data.Role;
            await _context.SaveChangesAsync();
            return existUser;
        }

        
    }
}