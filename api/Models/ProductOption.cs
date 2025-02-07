using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace api.Models
{
    [Table("ProductOptions")]
    public class ProductOption
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(64 * 2)]
        public required string Name { get; set; }
        public Guid? ProductDetailId { get; set; }
        [JsonIgnore]
        public ProductDetail? ProductDetail { get; set; }
    }
}