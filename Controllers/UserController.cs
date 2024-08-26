using Microsoft.AspNetCore.Mvc;
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



    }
}