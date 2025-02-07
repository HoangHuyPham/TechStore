using api.Models;

namespace api.Repos.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> FindByEmail(string email);
    }
}