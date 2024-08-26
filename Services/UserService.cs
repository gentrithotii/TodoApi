using ToDoApi.Data;
using ToDoApi.Models;
using ToDoApi.Models.Dtos;

namespace ToDoApi.Services
{
    public class UserService
    {
        private readonly ToDoContext _context;

        public UserService(ToDoContext context)
        {
            _context = context;
        }

        public async Task<User> CreateUserAsync(UserDto requestUser)
        {
            if (requestUser == null)
            {
                throw new ArgumentException(null, nameof(requestUser));
            }

            User user = new User
            {
                Email = requestUser.Email,
                PasswordHash = requestUser.Password
            };

            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}