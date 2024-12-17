using System.ComponentModel.DataAnnotations;

namespace api.DTOs.ProductOption
{
    public class ProductOptionCreateDTO
    {
        [StringLength(64)]
        public required string Name { get; set; }
    }
}