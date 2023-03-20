using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Projecto_Final.Models.OrderDTOs
{
    public class OrderCreateDTO
    {
        public string? ClientName { get; set; }
        public string? AddressLine { get; set; }
        [Required]
        public Status_Order Status { get; set; }

        public Guid? UserId { get; set; }
        public List<int> OrderIds { get; set; }
    }
}
