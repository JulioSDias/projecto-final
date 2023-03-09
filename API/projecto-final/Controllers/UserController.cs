using Microsoft.AspNetCore.Mvc;
using Projecto_Final.Models.UserDTOs;
using Projecto_Final.Services;

namespace Projecto_Final.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll() {

            var users = await _userService.GetAllUsers();
            return Ok(users);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDTO newUser, int roleId) {

            if (await _userService.CreateUser(newUser, roleId) == true)
                return Ok("user created.");
            return BadRequest("failed to create user.");
        }

        [HttpGet("getbyusername/{username}")]
        public async Task<IActionResult> GetByUsername(string username)
        {
            var user = await _userService.GetByUsername(username);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpGet("delete/{username}")]
        public async Task<IActionResult> Delete(string username) 
        {
            if (await _userService.Delete(username) == true)
                return Ok("User deleted.");
            return NotFound("user not found.");
        }

    }
}
