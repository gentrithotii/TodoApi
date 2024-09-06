using Microsoft.AspNetCore.Mvc;
using ToDoApi.Models;
using ToDoApi.Models.Dtos;
using ToDoApi.Services;
using ToDoApi.Services.InterfaceServices;

namespace ToDoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userSverice)
        {
            _userService = userSverice;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> RegisterUserAsync(UserDto user)
        {
            if (user == null) return BadRequest(user);

            var createdUser = await _userService.CreateUserAsync(user);

            return Ok(createdUser);
        }

        [HttpPost("login")]
        public async Task<ActionResult<User>> LogInUserAsync(UserDto user)
        {
            var token = await _userService.LogInUser(user);
            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(new { Token = token });
        }
    }
}