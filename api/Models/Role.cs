using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    [Table("Roles")]
    public class Role
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(256 * 2)]
        public required string URL { get; set; }
        public ICollection<UserGroup> UserGroups {get;} = [];
    }
}