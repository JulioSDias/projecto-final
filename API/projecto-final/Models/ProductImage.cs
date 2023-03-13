using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projecto_Final.Models
{
    public class ProductImage
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public byte[] Image { get; set; }
        [Required]
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset? ModifiedDate { get; set; }
        
        [Key]
        public int? ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product? Product { get; set; }
    }
}
