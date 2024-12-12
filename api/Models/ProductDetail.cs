using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    [Table("ProductDetails")]
    public class ProductDetail
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(256 * 2)]
        public string? Description { get; set; }
        public int Stock { get; set; }
        public float BeforePrice { get; set; }
        public float Price { get; set; }
        public float TotalRating { get; set; }
        public Guid ProductId { get; set; }
        public required Product Product { get; set; }
        public ICollection<Preview> Previews { get; set; } = [];
        public ICollection<ProductOption> ProductOptions { get; set; } = [];
        public ICollection<Review> Reviews{ get; set; } = [];
    }
}