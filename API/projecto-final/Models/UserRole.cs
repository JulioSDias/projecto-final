using System.ComponentModel.DataAnnotations;

namespace Projecto_Final.Models
{
    public class UserRole
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string RoleName { get; set; }
        [Required]
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset? ModifiedDate { get; set; }

        //public List<User>? Users { get; set; }
    }
}
