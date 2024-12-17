using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    [Table("Previews")]
    public class Preview
    {
        [Key]
        public Guid Id { get; set; }
        public required string URL { get; set; }
        public Guid? ProductDetailId { get; set; }
        public ProductDetail? ProductDetail { get; set; }
    }
}