using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projecto_Final.Services;

namespace Projecto_Final.Controllers
{
    [Route("api/genre")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var genres = await _genreService.GetAll();
            return Ok(genres);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var genre = await _genreService.GetbyId(id);
            if (genre == null) return NotFound();
            return Ok(genre);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] string newGenre)
        {
            if (await _genreService.Create(newGenre) == true)
                return Ok("genre created.");
            return BadRequest("failed to create genre.");
        }


        [HttpDelete("deletebyID/{id}")]
        public async Task<IActionResult> DeleteByID(int id)
        {
            if (await _genreService.DeleteById(id) == true)
                return Ok("genre deleted.");
            return NotFound("genre not found.");
        }
    }
}
