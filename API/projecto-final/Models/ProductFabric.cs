using System.ComponentModel.DataAnnotations;

namespace Projecto_Final.Models
{
    public class ProductFabric
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset? ModifiedDate { get; set; }

        //public List<ProductCategory>? Categories { get; set; }
    }
}
