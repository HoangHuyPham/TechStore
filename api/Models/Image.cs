using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    [Table("Images")]
    public class Image
    {
        [Key]
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string?  Url { get; set; }
    }
}