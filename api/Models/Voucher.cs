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
        public string? Name { get; set; }
        public Guid? Code { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? ExpiredAt { get; set; }
        public float? Factor { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}