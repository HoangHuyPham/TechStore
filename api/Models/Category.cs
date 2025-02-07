using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace api.Models
{
    [Table("Categories")]
    public class Category
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(256 * 2)] [JsonIgnore]
        public ICollection<Product> Products { get; set; } = [];
        public required string Name { get; set; }
    }
}