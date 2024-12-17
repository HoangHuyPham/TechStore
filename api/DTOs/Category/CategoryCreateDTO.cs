using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Product
{
    public class CategoryCreateDTO
    {
        [StringLength(256)]
        public required string Name { get; set; }
    }
}