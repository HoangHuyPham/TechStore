using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Order
{
    public class OrderUpdateDTO
    {
        public Guid Id { get; set; }
        public Guid? OrderTypeId { get; set; }
    }
}