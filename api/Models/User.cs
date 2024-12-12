using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    [Table("Users")]
    public class User(){
        [Key]
        public Guid Id { get; set;}
        [MaxLength(256 * 2)]
        public required string Name { get; set;}
        [MaxLength(256 * 2)]
        public required string Address { get; set;}
        [MaxLength(256 * 2)]
        public required string Email { get; set;}  
        public int Phone { get; set;}
        public string? Avatar { get; set;}
        public Account? Account { get; set;}
        public Review? Review{ get; set;}
        public required Guid UserGroupId;
        public required UserGroup UserGroup{ get; set;}
        public ICollection<Order>? Orders{ get; set;} = [];
        public ICollection<Cart>? Carts { get;} = [];
        public DateTime? CreatedOn { get; set;} = DateTime.Now;
    }
}