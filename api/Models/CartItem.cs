using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace api.Models
{
    [Table("CartItems")]
    public class CartItem
    {
        [Key]
        public Guid Id { get; set; }
        public required int Quantity { get; set; }
        public required bool IsSelected { get; set; }
        public Guid? CartId { get; set; }
        [JsonIgnore]
        public Cart? Cart { get; set; }
        [JsonIgnore]
        public Guid ProductId { get; set; }
        public Product? Product { get; set; }
        public Guid? OrderId { get; set; }
        [JsonIgnore]
        public Order? Order { get; set; }
        public Guid? ProductOptionId { get; set; }
        public ProductOption? ProductOption { get; set; }
    }
}