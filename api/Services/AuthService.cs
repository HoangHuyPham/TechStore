using api.DTOs.Auth;
using api.Models;
using api.Repos.Interfaces;
using api.Services.Interfaces;
namespace api.Services
{
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using api.Datas;
    using BCrypt.Net;
    using Microsoft.IdentityModel.Tokens;

    public class AuthService(ApplicationContext context, IUserRepository repoUser, IRepository<Role> repoRole, IRepository<Cart> repoCart, IConfiguration configuration) : IAuthService
    {
        private readonly IUserRepository _repoUser = repoUser;
        private readonly IRepository<Role> _repoRole = repoRole;
        private readonly IRepository<Cart> _repoCart = repoCart;
        private readonly IConfiguration _configuration = configuration;
        private readonly ApplicationContext _context = context;
        public async Task<User?> Register(RegisterDTO registerDTO)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var existUser = await _repoUser.FindByEmail(registerDTO.Email);
                if (existUser != null) return null;

                var newUser = await _repoUser.Create(new User
                {
                    Email = registerDTO.Email,
                    Name = registerDTO.Name,
                    Password = BCrypt.HashPassword(registerDTO.Password),
                    Role = await _repoRole.FindById(Guid.Parse("f80eee5a-eefe-49c6-9a11-2e5b3804a71c")),
                    Gender = registerDTO.Gender
                });

                if (newUser == null) return null;

                var newCart = await _repoCart.Create(new Cart
                {
                    UserId = newUser.Id
                });

                await transaction.CommitAsync();

                return newUser;
            }
            catch
            {
                transaction.Rollback();
            }
            return null;
        }

        public async Task<bool> UserAvailable(CheckEmailDTO checkEmailDTO)
        {
            var existUser = await _repoUser.FindByEmail(checkEmailDTO.Email);
            if (existUser != null) return true;
            return false;
        }

        public async Task<bool> ChangePassword(Guid userId, ChangePasswordDTO changePasswordDTO)
        {
            var existUser = await _repoUser.FindById(userId);
            if (existUser == null) return false;

            if (!BCrypt.Verify(changePasswordDTO.OldPassword, existUser.Password)) return false;
            existUser.Password = BCrypt.HashPassword(changePasswordDTO.NewPassword);
            var result = await _repoUser.Update(userId, existUser);
            if (result == null){
                return false;
            }
            return true;
        }
        public async Task<User?> Login(LoginDTO loginDTO)
        {
            var existUser = await _repoUser.FindByEmail(loginDTO.Email);
            if (existUser == null) return null;
            if (existUser.Cart == null)
                await _repoCart.Create(new Cart
                {
                    UserId = existUser.Id
                });


            if (!BCrypt.Verify(loginDTO.Password, existUser.Password)) return null;
            return existUser;
        }

        public string? generateToken(User user)
        {
            if (user.Role == null) return null;
            List<Claim> claims = [
                new Claim("Id", user.Id.ToString()),
                new Claim("Role", user.Role.Id.ToString()),
                new Claim("CartId", user.Cart?.Id.ToString()!)
            ];

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JWT:SecretToken").Value!));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: cred
            );

            var normalizeToken = new JwtSecurityTokenHandler().WriteToken(token);
            return normalizeToken;
        }
    }
}