using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    [Table("UserGroups")]
    public class UserGroup
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(256 * 2)]
        public required string Name { get; set; }
        public ICollection<User> Users { get;} = [];
        public ICollection<Role> Roles { get;} = [];
    }
}