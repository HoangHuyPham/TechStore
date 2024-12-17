using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(256 * 2)]
        public required string Name { get; set; }
        public string? Thumbnail { get; set; }
        public ICollection<CartItem> SelectedItems { get; set; } = [];
        public Category? Category { get; set; }
        public required ProductDetail ProductDetail { get; set; }
        public DateTime? CreatedOn { get; set;} = DateTime.Now;
    }
}