using api.DTOs.Auth;
using api.Models;

namespace api.Services.Interfaces
{
    public interface IAuthService
    {
        Task<User?> Login(LoginDTO loginDTO);
        Task<User?> Register(RegisterDTO registerDTO);
        Task<bool> UserAvailable(string email);
        string? generateToken(User user);
    }
}