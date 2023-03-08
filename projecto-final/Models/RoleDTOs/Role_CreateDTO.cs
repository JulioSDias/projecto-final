using System.ComponentModel.DataAnnotations;

namespace Projecto_Final.Models.RoleDTOs
{
    public class RoleCreateDTO
    {
        [Required]
        public string Name { get; set; }
    }
}
