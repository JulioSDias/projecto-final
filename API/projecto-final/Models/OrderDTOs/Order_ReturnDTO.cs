using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Projecto_Final.Models.OrderDTOs
{
    public class OrderReturnDTO
    {
        [Required]
        public Guid Id { get; set; }
        public string? ClientName { get; set; }
        public string? AddressLine { get; set; }
        [Required]
        public Status_Order Status { get; set; }
        [Required]
        [Precision(12, 2)]
        public decimal Total { get; set; }
        [Required]
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset? ModifiedDate { get; set; }

        public Guid? UserId { get; set; }
    }
}
