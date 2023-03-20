using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Projecto_Final.Models.OrderDTOs
{
    public class ItemCreateDTO
    {
        [Required]
        public int Quantity { get; set; } = 1;
        [Required]
        [Precision(12, 2)]
        public decimal Price { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public Guid OrderId { get; set; }


    }
}
