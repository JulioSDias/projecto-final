using Microsoft.AspNetCore.Mvc;
using Projecto_Final.Models.RoleDTOs;
using Projecto_Final.Models.UserDTOs;
using Projecto_Final.Services;

namespace Projecto_Final.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("getallusers")]
        public async Task<IActionResult> GetAll() {

            var users = await _userService.GetAllUsers();
            return Ok(users);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]UserRegisterDTO newUser, int roleId) {
            
            if(await _userService.CreateUser(newUser, roleId) == true)
                return Ok();
            return BadRequest();    
        }

        [HttpPost("addRole")]
        public async Task<IActionResult> AddRole(RoleCreateDTO newRole) {
            
            if (await _userService.CreateRole(newRole) == true)
                return Ok();
            return BadRequest();
        }

        [HttpGet("getallroles")]
        public async Task<IActionResult> GetAllRoles() {

            var roles = await _userService.GetAllRoles();
            return Ok(roles);
        }

        [HttpDelete("delete/{id}")]

        public async Task<IActionResult> DeleteRole(int ID) {
            if (await _userService.DeleteRole(ID) == true)
                return Ok();
            return BadRequest();
        }
    }
}
