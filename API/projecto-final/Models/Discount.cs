using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Projecto_Final.Models
{
    public class Discount
    {
        [Key]
        public int Id { get; set; }
        public string? Description { get; set; }
        [Required]
        [Precision(12, 2)]
        public decimal DiscountPercent { get; set; }
        [Required]
        public bool Active { get; set; }
        [Required]
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset? ModifiedDate { get; set; }

        //public List<Product>? Products { get; set; }

    }
}
