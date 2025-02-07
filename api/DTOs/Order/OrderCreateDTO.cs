using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Order
{
    public class OrderCreateDTO
    {
        [MaxLength(1024)]
        public string? Description { get; set; }
        public Guid? OrderTypeId { get; set; }
        public Guid? VoucherId { get; set;}
        public ICollection<Guid> Items { get; set; } = [];
    }
}