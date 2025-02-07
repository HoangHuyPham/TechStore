using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace api.Models
{
    [Table("Carts")]
    public class Cart
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; } = null!;
        public ICollection<CartItem>? CartItems { get; } = [];
    }
}