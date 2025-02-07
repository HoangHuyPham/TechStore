using api.Models;

namespace api.DTOs.User
{
    public class UserDTO
    {
        public Guid Id { get; set;}
        public string? Name { get; set;}
        public string? Address { get; set;}
        public string? Email { get; set;}
        public int? Phone { get; set;}
        public bool? Gender { get; set;}
        public Models.Image? Avatar { get; set;}
        public Role? Role { get; set;}
        public Review? Review{ get; set;}
        public ICollection<Models.Order>? Orders{ get; set;} = [];
        public DateTime? CreatedOn { get; set;} = DateTime.Now;
    }
}