using Projecto_Final.Models;

namespace Projecto_Final.Services
{
    public interface IConsoleService
    {
        Task<IEnumerable<GameConsole>> GetAll();
        Task<bool> Create(string consoleName);
        Task<GameConsole> GetbyId(int id);
        Task<bool> DeleteById(int id);
    }
}
