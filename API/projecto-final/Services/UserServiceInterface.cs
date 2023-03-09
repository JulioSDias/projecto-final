using Projecto_Final.Models;
using Projecto_Final.Models.UserDTOs;

namespace Projecto_Final.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<bool> CreateUser(UserRegisterDTO newUser, int RoleId);
        Task<UserReturnDTO> GetByUsername(string username);
        Task<bool> Delete(string username);

    }
}
