using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    [Table("Previews")]
    public class Preview
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(256 * 2)]
        public required string URL { get; set; }
        public required Guid ProductDetailId { get; set; }
        public required ProductDetail ProductDetail { get; set; }
    }
}