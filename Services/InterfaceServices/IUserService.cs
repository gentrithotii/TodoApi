using ToDoApi.Models;
using ToDoApi.Models.Dtos;

namespace ToDoApi.Services.InterfaceServices
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(UserDto reqUser);
        Task<string> LogInUser(UserDto reqUser);

    }
}

