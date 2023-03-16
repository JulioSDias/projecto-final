using Microsoft.EntityFrameworkCore;
using Projecto_Final.Contexts;
using Projecto_Final.Models;
using Projecto_Final.Models.GenreDTOs;
using Projecto_Final.Models.ProductDTOs;

namespace Projecto_Final.Services
{
    public class ProductService : IProductService
    {
        private readonly StoreContext _context;
        private readonly IConsoleService _consoleService;
        private readonly IGenreService _genreService;
        private readonly IDiscountService _discountService;

        public ProductService(StoreContext context, IConsoleService consoleService, IDiscountService discountService, IGenreService genreService)
        {
            _context = context;
            _consoleService = consoleService;
            _discountService = discountService;
            _genreService = genreService;
        }

        public async Task<IEnumerable<ProductReturnDTO>> GetAll()
        {
            var products = await _context.Products.Include(c => c.Console).Include(d => d.Discount).ToListAsync();
            var returnProducts = new List<ProductReturnDTO>();

            foreach (var product in products)
            {
                var productGenres = await _context.ProductGenres.Where(i => i.ProductId == product.Id).ToListAsync();

                var genres = new List<GenreReturnDTO>();
                foreach (var productGenre in productGenres)
                {
                    var genre = await _genreService.GetbyId(productGenre.GenreId);
                    var returnGenre = new GenreReturnDTO
                    {
                        Id = genre.Id,
                        Name = genre.Name,
                        CreatedDate = genre.CreatedDate,
                        ModifiedDate = genre.ModifiedDate
                    };
                    genres.Add(returnGenre);
                }

                var returnProduct = new ProductReturnDTO
                {
                    Id =product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Stock = product.Stock,
                    Description = product.Description,
                    Discount = product.Discount,
                    Console = product.Console,
                    Genres = genres
                };
                returnProducts.Add(returnProduct);
            }
            return returnProducts;
        }

        public async Task<bool> Create(ProductCreateDTO newProduct)
        {
            var console = await _consoleService.GetbyId(newProduct.ConsoleId);
            var discount = await _discountService.GetbyId(newProduct.DiscountId);

            var DBproduct = new Product
            {
                Name = newProduct.Name,
                Price = newProduct.Price,
                Stock = newProduct.Stock,
                Description = newProduct.Description,
                CreatedDate = DateTimeOffset.Now,
                ConsoleId = newProduct.ConsoleId,
                Console = console,
                DiscountId = newProduct.DiscountId,
                Discount = discount,
                Genres = null
            };

            await _context.Products.AddAsync(DBproduct);
            await _context.SaveChangesAsync();

            foreach (int id in newProduct.GenresId)
            {
                var genre = await _genreService.GetbyIdFull(id);

                var DBproductGenre = new ProductGenre
                {
                    GenreId = id,
                    Genre = genre,
                    ProductId = DBproduct.Id,
                    Product = DBproduct
                };

                await _context.ProductGenres.AddAsync(DBproductGenre);
            }

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<ProductReturnDTO> GetbyId(int id)
        {

            var DBproduct = await _context.Products.Include(c => c.Console).Include(d => d.Discount).FirstOrDefaultAsync(i => i.Id == id);
            if (DBproduct == null) return null;

            var productGenres = await _context.ProductGenres.Where(i => i.ProductId == DBproduct.Id).ToListAsync();

            var genres = new List<GenreReturnDTO>();
            foreach (var productGenre in productGenres)
            {
                var genre = await _genreService.GetbyId(productGenre.GenreId);
                var returnGenre = new GenreReturnDTO
                {
                    Id = genre.Id,
                    Name = genre.Name,
                    CreatedDate = genre.CreatedDate,
                    ModifiedDate = genre.ModifiedDate
                };
                genres.Add(returnGenre);
            }

            var returnProduct = new ProductReturnDTO
            {
                Id = DBproduct.Id,
                Name = DBproduct.Name,
                Price = DBproduct.Price,
                Stock = DBproduct.Stock,
                Description = DBproduct.Description,
                Discount = DBproduct.Discount,
                Console = DBproduct.Console,
                Genres = genres
            };
            return returnProduct;
        }

        public async Task<bool> Delete(int id)
        {

            var DBproduct = await _context.Products.FindAsync(id);
            if (DBproduct == null) return false;

            var connectionTable = await _context.ProductGenres.Where(p => p.ProductId == DBproduct.Id).ToListAsync();
            foreach (var connection in connectionTable)
                _context.ProductGenres.Remove(connection);

            _context.Products.Remove(DBproduct);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<ProductGenre>> GetAllConnections()
        {
            return await _context.ProductGenres.ToListAsync();
        }

        public async Task<bool> Update(ProductUpdateDTO prodUpdate)
        {
            var DBproduct = await _context.Products.Include(d => d.Discount).FirstOrDefaultAsync(i => i.Id == prodUpdate.Id);
            if (DBproduct == null) return false;
            Discount discount = null;
            if (prodUpdate.DiscountId != null)
                discount = await _discountService.GetbyId((int)prodUpdate.DiscountId);

            if (prodUpdate.DiscountId == 0)
                prodUpdate.DiscountId = null;

            DBproduct.Status = prodUpdate.Status;
            DBproduct.Price = prodUpdate.Price;
            DBproduct.Stock = prodUpdate.Stock;
            DBproduct.Description = prodUpdate.Description;
            DBproduct.DiscountId = prodUpdate.DiscountId;
            DBproduct.Discount = discount;
            DBproduct.ModifiedDate = DateTimeOffset.Now;

            _context.Products.Entry(DBproduct).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
