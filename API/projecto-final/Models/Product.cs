using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projecto_Final.Models
{
    public enum Status_product { 
        ok,
        disabled,
        out_of_stock,
    }
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public Status_product Status { get; set; }
        [Required]
        [Precision(12, 2)]
        public decimal Price { get; set; }
        [Required]
        public int Stock { get; set; }
        public string? Description { get; set; }
        [Required]
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset? ModifiedDate { get; set; }

        public int ConsoleId { get; set; }
        [ForeignKey("ConsoleId")]
        public GameConsole Console { get; set; }

        public int? DiscountId { get; set; }
        [ForeignKey("DiscountId")]
        public Discount? Discount { get; set; }

        public List<ProductGenre> Genres { get; set; }

    }
}
