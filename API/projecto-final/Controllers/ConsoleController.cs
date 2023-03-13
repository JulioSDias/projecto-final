using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projecto_Final.Models.DiscountDTOs;
using Projecto_Final.Services;

namespace Projecto_Final.Controllers
{
    [Route("api/console")]
    [ApiController]
    public class ConsoleController : ControllerBase
    {
        private readonly IConsoleService _consoleService;

        public ConsoleController(IConsoleService consoleService)
        {
            _consoleService = consoleService;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var consoles = await _consoleService.GetAll();
            return Ok(consoles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var console = await _consoleService.GetbyId(id);
            if (console == null) return NotFound();
            return Ok(console);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] string newConsole)
        {
            if (await _consoleService.Create(newConsole) == true)
                return Ok("console created.");
            return BadRequest("failed to create console.");
        }


        [HttpDelete("deletebyID/{id}")]
        public async Task<IActionResult> DeleteByID(int id)
        {
            if (await _consoleService.DeleteById(id) == true)
                return Ok("console deleted.");
            return NotFound("console not found.");
        }

    }
}

