using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Auth
{
    public class RegisterDTO
    {
        public string Name { get; set; } = null!;
        [EmailAddress]
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public bool? Gender { get; set;} = true;
    }
}