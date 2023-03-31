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
        public async Task<IActionResult> Register([FromBody] UserRegisterDTO newUser) {

            if (await _userService.CreateUser(newUser) == true)
                return Ok();
            return BadRequest("failed to create user.");
        }

        [HttpGet("getby/{username}")]
        public async Task<IActionResult> GetByUsername(string username)
        {
            var user = await _userService.GetByUsername(username);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpGet("getbyID/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _userService.GetById(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpGet("getbyrole/{rolename}")]
        public async Task<IActionResult> GetByRole(string rolename) { 
            var usernames = await _userService.GetByRole(rolename);
            if (usernames == null) return Ok("no user on that role.");
            return Ok(usernames);
        }

        [HttpDelete("deleteby/{username}")]
        public async Task<IActionResult> DeletebyUsername(string username) 
        {
            if (await _userService.DeleteByUsername(username) == true)
                return Ok("User deleted.");
            return NotFound("user not found.");
        }

        [HttpDelete("deletebyID/{id}")]
        public async Task<IActionResult> DeleteByID(Guid id)
        {
            if (await _userService.DeleteById(id) == true)
                return Ok();
            return NotFound("user not found.");
        }

        [HttpPut("updateRole")]
        public async Task<IActionResult> UpdateRole(UserUpdateRoleDTO newChanges) {
            if (await _userService.ChangeRole(newChanges) == false)
                return NotFound();
            return Ok();
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UserUpdateDTO userChanges)
        {
            if (await _userService.Update(id, userChanges) == false)
                return NotFound();
            return Ok();
        }

        [HttpPost("Authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] UserLoginDTO userLogin) {
            var user = await _userService.Authenticate(userLogin.Username, userLogin.Password);
            if (user == null)
                return BadRequest("wrong password or username.");
            return Ok(user);
        }
        
    }
}
