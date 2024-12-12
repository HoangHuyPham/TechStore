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
        public ICollection<CartItem> SelectedItems { get; set; } = [];
        public ICollection<Category> Categories { get; set; } = [];
        public ProductDetail? ProductDetail { get; set; }
        public DateTime? CreatedOn { get; set;} = DateTime.Now;
    }
}