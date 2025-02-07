using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Auth
{
    public class CheckEmailDTO
    {
        public required string Email { get; set; } = null!;
    }
}