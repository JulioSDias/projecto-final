using Projecto_Final.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Projecto_Final.Models.ImageDTOs
{
    public class ImageUploadDTO
    {
        
        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Upload)]
        [AllowedExtensions(new string[] { ".jpeg", ".jpg", ".png" })]
        public IFormFile Image { get; set; }
        
        public int? ProductId { get; set; }
       
    }
}
