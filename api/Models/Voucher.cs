using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    [Table("Vouchers")]
    public class Voucher
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(256 * 2)]
        public required string Name { get; set; }
        public required bool IsActive { get; set; }
        public required DateTime ExpiredAt { get; set; }
        public required float Factor { get; set; }
        public Order? Order{ get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}