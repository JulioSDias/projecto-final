using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Projecto_Final.Models.GenreDTOs;
using Projecto_Final.Models.ImageDTOs;
using System.ComponentModel.DataAnnotations;

namespace Projecto_Final.Models.ProductDTOs
{
    public class ProductReturnDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Precision(12, 2)]
        public decimal Price { get; set; }
        [Required]
        public int Stock { get; set; }
        public string? Description { get; set; }
        [Required]
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset? ModifiedDate { get; set; }
        [Required]
        public GameConsole Console { get; set; }

        public Discount Discount { get; set; }
        [Required]
        public List<GenreReturnDTO> Genres { get; set; }

        public List<ImageReturnDTO> Images { get; set; }
    }
}
