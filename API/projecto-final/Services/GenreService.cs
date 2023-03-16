using Microsoft.EntityFrameworkCore;
using Projecto_Final.Contexts;
using Projecto_Final.Models;
using Projecto_Final.Models.GenreDTOs;

namespace Projecto_Final.Services
{
    public class GenreService : IGenreService
    {
        private readonly StoreContext _context;
        public GenreService(StoreContext context)
        {

            _context = context;
        }

        public async Task<IEnumerable<GenreReturnAllDTO>> GetAll()
        {
            var allGenres = new List<GenreReturnAllDTO>();

            var genres = await _context.Genres.Include(p => p.Products).ToListAsync();

            foreach (var genre in genres)
            {
                var productNames = new List<string>();
                foreach (var productGenre in genre.Products)
                {
                    var product = await _context.Products.FindAsync(productGenre.ProductId);
                    productNames.Add(product.Name);
                }
                var returnGenre = new GenreReturnAllDTO
                {
                    Id = genre.Id,
                    Name = genre.Name,
                    CreatedDate = genre.CreatedDate,
                    ModifiedDate = genre.ModifiedDate,
                    ProductNames = productNames
                };
                allGenres.Add(returnGenre);
            }
            return allGenres;
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

        public async Task<GenreReturnAllDTO> GetbyId(int id)
        {
            var genre = await _context.Genres.Include(p => p.Products).FirstOrDefaultAsync(i => i.Id == id);
            if ( genre == null) return null;

            var productNames = new List<string>();
            foreach (var productGenre in genre.Products) {
                var product = await _context.Products.FindAsync(productGenre.ProductId);
                productNames.Add(product.Name);
            }
            var returnGenre = new GenreReturnAllDTO { 
                Id = genre.Id,
                Name = genre.Name,
                CreatedDate = genre.CreatedDate,
                ModifiedDate = genre.ModifiedDate,
                ProductNames = productNames
            };

            return returnGenre;
        }

        public async Task<Genre> GetbyIdFull(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
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
