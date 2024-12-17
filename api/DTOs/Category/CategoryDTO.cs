using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Product
{
    public class CategoryDTO
    {
        public Guid Id { get; set; }
        [StringLength(256)]
        public required string Name { get; set; }
    }
}