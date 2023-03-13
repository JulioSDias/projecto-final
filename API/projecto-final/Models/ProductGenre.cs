using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projecto_Final.Models
{
    public class ProductGenre
    {
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        
        public int GenreId { get; set; }
        [ForeignKey("GenreId")]
        public Genre Genre { get; set; }
    }
}
