using Projecto_Final.Models;
using Projecto_Final.Models.UserDTOs;

namespace Projecto_Final.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<bool> CreateUser(UserRegisterDTO newUser);
        Task<UserReturnDTO> GetByUsername(string username);
        Task<bool> DeleteByUsername(string username);
        Task<bool> DeleteById(Guid id);
        Task<bool> ChangeRole(UserUpdateRoleDTO newChanges);
        Task<bool> Update(Guid id, UserUpdateDTO userChanges);
        Task<List<string>> GetByRole(string rolename);
        Task<UserReturnDTO> GetById(Guid id);
        Task<UserAuthenticateDTO?> Authenticate(string Username, string Password);

    }
}
