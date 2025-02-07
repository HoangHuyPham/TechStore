using System.ComponentModel.DataAnnotations;

namespace api.Query
{
    public class ProductQuery : AQuery
    {
        public Guid? CategoryId { get; set; }
        [Range(0, 1_000_000_000)]
        public float MinPrice { get; set; } = 0;
        [Range(0, 1_000_000_000)]
        public float MaxPrice { get; set; } = 1_000_000_000;
    }
}