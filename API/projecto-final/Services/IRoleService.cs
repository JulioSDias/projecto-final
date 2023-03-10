using Projecto_Final.Models;

namespace Projecto_Final.Services
{
    public interface IRoleService
    {
        Task<bool> Create(string newRole);
        Task<IEnumerable<UserRole>> GetAll();
        Task<bool> Delete(int id);
    }
}
