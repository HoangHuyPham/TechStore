using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    [Table("Accounts")]
    public class Account
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(256 * 2)]
        public required string Username { get; set; }
        [MaxLength(256 * 2)]
        public required string Password { get; set; }
        public required Guid UserId { get; set; }
        public required User User { get; set; }
    }
}