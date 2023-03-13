using Projecto_Final.Models;

namespace Projecto_Final.Services
{
    public interface IGenreService
    {
        Task<IEnumerable<Genre>> GetAll();
        Task<bool> Create(string genreName);
        Task<Genre> GetbyId(int id);
        Task<bool> DeleteById(int id);
    }
}
