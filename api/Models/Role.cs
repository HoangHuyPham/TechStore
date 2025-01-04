using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    [Table("Roles")]
    public class Role
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [MaxLength(256 * 2)]
        public string? Name { get; set; }
    }
}