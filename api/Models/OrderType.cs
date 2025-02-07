using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace api.Models
{
    [Table("OrderTypes")]
    public class OrderType
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(128 * 2)]
        public required string Name { get; set; }
        [JsonIgnore]
        public ICollection<Order> Orders { get; set; } = [];
    }
}