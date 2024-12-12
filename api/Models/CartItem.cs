using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    [Table("CartItems")]
    public class CartItem
    {
        [Key]
        public Guid Id { get; set; }
        public required short Quantity { get; set; }
        public required bool IsSelected { get; set; }
        public Guid CartId {get; set;}
        public required Cart Cart { get; set; }
        public Guid ProductId { get; set; }
        public required Product Product { get; set; }
        public Guid? OrderId { get; set; }
        public Order? Order { get; set; }
       

    }
}