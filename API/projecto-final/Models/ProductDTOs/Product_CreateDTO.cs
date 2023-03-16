using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Projecto_Final.Models.ProductDTOs
{
    public class ProductCreateDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [Precision(12, 2)]
        public decimal Price { get; set; }
        [Required]
        public int Stock { get; set; }
        public string? Description { get; set; }
        [Required]
        public int ConsoleId { get; set; }
        public int DiscountId { get; set; }
        [Required]
        public List<int> GenresId { get; set; }
    }
}
