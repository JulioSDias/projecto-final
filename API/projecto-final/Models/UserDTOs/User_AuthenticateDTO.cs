using System.ComponentModel.DataAnnotations;

namespace Projecto_Final.Models.UserDTOs
{
    public class UserAuthenticateDTO
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Token { get; set; }
    }
}
