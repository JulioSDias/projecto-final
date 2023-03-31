using System.ComponentModel.DataAnnotations;

namespace Projecto_Final.Models.UserDTOs
{
    public class UserUpdateDTO
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string? Password { get; set; }
        [Required]
        public int SocialSecurity { get; set; }
        [Required]
        public int PhoneNumber { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string PostalCode { get; set; }
        [Required]
        public string RoleName { get; set; }
    }
}
