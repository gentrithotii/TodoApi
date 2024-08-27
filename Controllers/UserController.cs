using Microsoft.AspNetCore.Mvc;
using ToDoApi.Models;
using ToDoApi.Models.Dtos;
using ToDoApi.Services;

namespace ToDoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userSverice)
        {
            _userService = userSverice;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> RegisterUserAsync(UserDto user)
        {
            if (user == null)
            {
                return BadRequest(user);
            }
            var createdUser = await _userService.CreateUserAsync(user);

            return Ok(createdUser);
        }


        // public async Task<ActionResult<User>> LogInUser(UserDto user)
        // {
        //     if (user == null)
        //     {
        //         return BadRequest(user);
        //     }
        //     var test = await _userService.LogInUser(user);
        //     var token = await _
        // }
    }
}