using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{

    public class User{
        [Key]
        public Guid Id { get; set;}
        [MaxLength(256 * 2)]
        public string Name { get; set;} = null!;
        [MaxLength(256 * 2)]
        public string? Address { get; set;}
        [MaxLength(256 * 2)]
        public string? Email { get; set;}
        public int? Phone { get; set;}
        public string? Avatar { get; set;}
        public string Password { get; set;} = null!;
        public Role? Role { get; set;}
        public Review? Review{ get; set;}
        public ICollection<Order>? Orders{ get; set;} = [];
        public ICollection<Cart>? Carts { get;} = [];
        public DateTime? CreatedOn { get; set;} = DateTime.Now;
    }
}