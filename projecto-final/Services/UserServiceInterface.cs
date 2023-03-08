using Projecto_Final.Models;
using Projecto_Final.Models.RoleDTOs;
using Projecto_Final.Models.UserDTOs;
using System.Threading.Tasks;

namespace Projecto_Final.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<bool> CreateUser(UserRegisterDTO newUser, int RoleId);
        Task<bool> CreateRole(RoleCreateDTO newRole);
        Task<IEnumerable<UserRole>> GetAllRoles();
        Task<bool> DeleteRole(int RoleId);

    }
}
