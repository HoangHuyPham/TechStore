using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    [Table("Categories")]
    public class Category
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(256 * 2)]
        public ICollection<Product> Products { get; set; } = [];
        public required string Name { get; set; }
    }
}