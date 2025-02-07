using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace api.Models
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(256 * 2)]
        public required string Name { get; set; }
        public Guid? ImageId { get; set; }
        public Guid? ThumbnailId { get; set; }
        public Image? Thumbnail { get; set; }
        [JsonIgnore]
        public ICollection<CartItem> SelectedItems { get; set; } = [];
        public Category? Category { get; set; }
        public required ProductDetail ProductDetail { get; set; }
        public DateTime? CreatedOn { get; set;} = DateTime.Now;
    }
}