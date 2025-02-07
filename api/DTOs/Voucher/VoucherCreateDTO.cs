using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Product
{
    public class VoucherCreateDTO
    {
        [MaxLength(256)]
        public string? Name { get; set; }
        public Guid? Code { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? ExpiredAt { get; set; }
        public float? Factor { get; set; }
    }
}