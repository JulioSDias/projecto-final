using Microsoft.AspNetCore.Mvc;
using Projecto_Final.Services;

namespace Projecto_Final.Controllers
{
    [Route("api/role")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost("add/{newRole}")]
        public async Task<IActionResult> Add(string newRole)
        {

            if (await _roleService.Create(newRole) == true)
                return Ok();
            return BadRequest();
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {

            var roles = await _roleService.GetAll();
            return Ok(roles);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _roleService.Delete(id) == true)
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
