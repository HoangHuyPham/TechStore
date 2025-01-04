using api.Models;

namespace api.Repos.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> FindById(Guid id);
        Task<User?> FindByEmail(string email);
        Task<List<User>> FindAll();
        Task<User?> Create(User target);
        Task<User?> Update(Guid id, User data);
        Task<User?> Delete(Guid id);
    }
}