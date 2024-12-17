using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    [Table("ProductDetails")]
    public class ProductDetail
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(256 * 2)]
        public string? Description { get; set; }
        public int Stock { get; set; }
        public float BeforePrice { get; set; }
        public float Price { get; set; }
        public float TotalRating { get; set; }
        public Guid? ProductId { get; set; }
        public Product? Product { get; set; }
        public ICollection<Preview>? Previews { get; set; }
        public ICollection<ProductOption>? ProductOptions { get; set; }
    }
}