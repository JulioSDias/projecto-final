using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Projecto_Final.Models.OrderDTOs
{
    public class ItemReturnDTO
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Quantity { get; set; } = 1;
        [Required]
        [Precision(12, 2)]
        public decimal Price { get; set; }
        [Required]
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset? ModifiedDate { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public Guid OrderId { get; set; }
    }
}
