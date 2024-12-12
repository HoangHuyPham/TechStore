using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    [Table("ProductOptions")]
    public class ProductOption
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(256 * 2)]
        public required string Name { get; set; }
        public required Guid ProductDetailId { get; set; }
        public required ProductDetail ProductDetail { get; set; }
    }
}