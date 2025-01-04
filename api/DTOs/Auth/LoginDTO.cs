using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Auth
{
    public class LoginDTO
    {
        [EmailAddress]
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}