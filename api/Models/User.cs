using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace api.Models
{

    public class User{
        [Key]
        public Guid Id { get; set;}
        [MaxLength(256 * 2)]
        public string? Name { get; set;} = null!;
        [MaxLength(256 * 2)]
        public string? Address { get; set;}
        [MaxLength(256 * 2)]
        public string? Email { get; set;}
        public int? Phone { get; set;}
        public Guid? AvatarId { get; set;}
        public Image? Avatar { get; set;}
        public string Password { get; set;} = null!;
        public bool? Gender { get; set;} = true;
        public Cart? Cart{ get; set;}
        public Role? Role { get; set;}
        public Review? Review{ get; set;}
        [JsonIgnore]
        public ICollection<Order>? Orders{ get; set;} = [];
        public DateTime? CreatedOn { get; set;} = DateTime.Now;
    }
}