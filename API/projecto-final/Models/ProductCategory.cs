#nullable disable

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projecto_Final.Models
{
    public enum sizes { 
        XS,
        S,
        M,
        L,
        XL,
        XXL,
    }
    public class ProductCategory
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public sizes Size { get; set; }
        public string? Description { get; set; }
        [Required]
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset? ModifiedDate { get; set; }

        public int FabricId { get; set; }
        [ForeignKey("FabricId")]
        public ProductFabric Fabric { get; set; }

        public int ColorId { get; set; }
        [ForeignKey("ColorId")]
        public ProductColor Color { get; set; }
    }
}
