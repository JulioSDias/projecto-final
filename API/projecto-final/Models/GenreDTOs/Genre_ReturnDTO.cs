using System.ComponentModel.DataAnnotations;

namespace Projecto_Final.Models.GenreDTOs
{
    public class GenreReturnDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset? ModifiedDate { get; set; }
    }
}
