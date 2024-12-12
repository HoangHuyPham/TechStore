using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    [Table("OrderTypes")]
    public class OrderType
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(128 * 2)]
        public required string Name { get; set; }
        public ICollection<Order> Orders { get; set; } = [];
    }
}