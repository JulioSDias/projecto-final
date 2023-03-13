using Microsoft.EntityFrameworkCore;
using Projecto_Final.Contexts;
using Projecto_Final.Models;

namespace Projecto_Final.Services
{
    public class GenreService : IGenreService
    {
        private readonly StoreContext _context;
        public GenreService(StoreContext context)
        {

            _context = context;
        }

        public async Task<IEnumerable<Genre>> GetAll()
        {
            return await _context.Genres.ToListAsync();
        }

        public async Task<bool> Create(string genreName)
        {

            var newGenre = new Genre
            {
                Name = genreName,
                CreatedDate = DateTimeOffset.Now
            };

            await _context.Genres.AddAsync(newGenre);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Genre> GetbyId(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if ( genre == null) return null;

            return genre;
        }

        public async Task<bool> DeleteById(int id)
        {
            var DBgenre = await _context.Genres.FindAsync(id);
            if (DBgenre == null) return false;

            _context.Genres.Remove(DBgenre);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
