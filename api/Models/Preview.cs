using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace api.Models
{
    [Table("Previews")]
    public class Preview
    {
        [Key]
        public Guid Id { get; set; }
        public Guid? ImageId { get; set; }
        public Image? Image { get; set; }
        public Guid? ProductDetailId { get; set; }
        [JsonIgnore]
        public ProductDetail? ProductDetail { get; set; }
    }
}