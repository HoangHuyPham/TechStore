using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace api.Models
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(1024 * 2)]
        public string? Description { get; set; }
        public Guid? OrderTypeId { get; set; }
        public OrderType? OrderType { get; set; }
        public Guid? BuyerId { get; set; }
        [JsonIgnore]
        public User? User { get; set; }
        public Guid? VoucherId { get; set; }
        public Voucher? Voucher { get; set; }
        public ICollection<CartItem>? CartItems { get; set; } = [];
        public DateTime? CreatedOn { get; set;} = DateTime.Now;
    }
}