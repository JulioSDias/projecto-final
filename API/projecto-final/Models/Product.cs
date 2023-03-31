using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projecto_Final.Models
{
    public enum Status_product { 
        OK,
        Disabled,
        Out_of_stock,
    }
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public Status_product Status { get; set; } = Status_product.OK;
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
        public List<ProductGenre>? Genres { get; set; }

    }
}
