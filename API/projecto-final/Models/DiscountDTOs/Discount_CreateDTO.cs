using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Projecto_Final.Models.DiscountDTOs
{
    public class DiscountCreateDTO
    {
        public string? Description { get; set; }
        [Required]
        [Precision(12, 2)]
        public decimal DiscountPercent { get; set; }
        [Required]
        public bool Active { get; set; }
    }
}
