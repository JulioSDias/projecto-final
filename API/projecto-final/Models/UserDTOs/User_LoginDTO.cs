using System.ComponentModel.DataAnnotations;

namespace Projecto_Final.Models.UserDTOs
{
    public class UserLoginDTO
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
