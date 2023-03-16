using Projecto_Final.Models;
using Projecto_Final.Models.GenreDTOs;

namespace Projecto_Final.Services
{
    public interface IGenreService
    {
        Task<IEnumerable<GenreReturnAllDTO>> GetAll();
        Task<bool> Create(string genreName);
        Task<GenreReturnAllDTO> GetbyId(int id);
        Task<Genre> GetbyIdFull(int id);
        Task<bool> DeleteById(int id);
    }
}
