using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projecto_Final.Models
{
    public enum Status_Order { 
        Complete,
        Pending,
        Transport,
        Waiting,
        Disable,
        Canceled,
    }
    public class Order
    {
        [Key]
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
        [ForeignKey("UserId")]
        public User? User { get; set; }
        [Required]
        public List<OrderItem> Items { get; set; }
    }
}
