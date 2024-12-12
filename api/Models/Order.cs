using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(1024 * 2)]
        public required string Description { get; set; }
        public required Guid OrderTypeId { get; set; }
        public required OrderType OrderType { get; set; }
        public required Guid BuyerId { get; set; }
        public required User User { get; set; }
        public Guid? VoucherId { get; set; }
        public Voucher? Voucher { get; set; }
        public ICollection<CartItem> CartItems { get; set; } = [];
        public DateTime? CreatedOn { get; set;} = DateTime.Now;
    }
}