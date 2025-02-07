using api.Models;

namespace api.DTOs.User
{
    public class UserUpdateDTO
    {
        public string? Name { get; set;}
        public string? Address { get; set;}
        public string? Email { get; set;}
        public int? Phone { get; set;}
        public string? Avatar { get; set;}
        public Role? Role { get; set;}
        public Review? Review{ get; set;}
        public ICollection<Models.Order>? Orders{ get; set;}
    }
}