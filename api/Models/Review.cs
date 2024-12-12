using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    [Table("Reviews")]
    public class Review
    {
        [Key]
        public Guid Id { get; set; }
        public required float Rating { get; set; }
        [MaxLength(1024 * 2)]
        public required string Comment { get; set; }
        public required Guid ProductDetailId { get; set; }
        public required ProductDetail ProductDetail { get; set; }
        public required Guid UserId { get; set; }
        public required User User { get; set; }
        public DateTime? CreatedOn { get; set;} = DateTime.Now;
    }
}