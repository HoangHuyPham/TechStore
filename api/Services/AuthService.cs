using api.DTOs.Auth;
using api.Models;
using api.Repos.Interfaces;
using api.Services.Interfaces;
namespace api.Services
{
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using BCrypt.Net;
    using Microsoft.IdentityModel.Tokens;

    public class AuthService (IUserRepository repoUser, IRepository<Role> repoRole, IConfiguration configuration) : IAuthService
    {
        private readonly IUserRepository _repoUser = repoUser;
        private readonly IRepository<Role> _repoRole = repoRole;
        private readonly IConfiguration _configuration = configuration;
        public async Task<User?> Register(RegisterDTO registerDTO){
            var existUser = await _repoUser.FindByEmail(registerDTO.Email);
            if (existUser != null) return null;

            var newUser = await _repoUser.Create(new User{
                Email = registerDTO.Email,
                Name = registerDTO.Name,
                Password = BCrypt.HashPassword(registerDTO.Password),
                Role = await _repoRole.FindById(Guid.Parse("f80eee5a-eefe-49c6-9a11-2e5b3804a71c"))
            });
   
            return newUser;
        }

        public async Task<bool> UserAvailable(string email){
            var existUser = await _repoUser.FindByEmail(email);
            if (existUser != null) return false;
            return true;
        }
        public async Task<User?> Login(LoginDTO loginDTO){
            var existUser = await _repoUser.FindByEmail(loginDTO.Email);
            if (existUser == null) return null;
            if (!BCrypt.Verify(loginDTO.Password, existUser.Password)) return null;
            return existUser;
        }

        public string? generateToken(User user){
            if ( user.Role == null ) return null;
            List<Claim> claims = [
                new Claim("Name", user.Name),
                new Claim("Role", user.Role.Id.ToString()),
            ];

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JWT:SecretToken").Value!));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                claims : claims,
                expires : DateTime.Now.AddHours(1),
                signingCredentials: cred
            );
            
            var normalizeToken = new JwtSecurityTokenHandler().WriteToken(token);
            return normalizeToken;
        }
    }
}