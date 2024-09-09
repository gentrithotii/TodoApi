using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ToDoApi.Data;
using ToDoApi.Models;
using ToDoApi.Models.Dtos;
using ToDoApi.Services.InterfaceServices;



namespace ToDoApi.Services
{
    public class UserService : IUserService
    {
        private readonly ToDoContext _context;
        private readonly IConfiguration _configuration;

        public UserService(ToDoContext context, IConfiguration configuration)
        {
            _configuration = configuration;
            _context = context;
        }

        public async Task<User> CreateUserAsync(UserDto requestUser)
        {
            var checkIfUserExist = await _context.Users.AnyAsync((u) => u.Email == requestUser.Email);

            if (checkIfUserExist) throw new ArgumentException("User already exists.");

            if (requestUser == null) throw new ArgumentException(null, nameof(requestUser));


            string hashPassword = BCrypt.Net.BCrypt.HashPassword(requestUser.PasswordHash);

            User user = new User
            {
                Email = requestUser.Email,
                PasswordHash = hashPassword
            };

            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<string> LogInUser(UserDto requestUser)
        {
            if (requestUser == null) throw new ArgumentNullException(nameof(requestUser), "Request user data is required.");

            var foundUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == requestUser.Email) ?? throw new Exception("User not found");

            if (foundUser.Email != requestUser.Email) throw new Exception("Email does not match");

            if (!BCrypt.Net.BCrypt.Verify(requestUser.PasswordHash, foundUser.PasswordHash)) throw new Exception("Password does not mach");

            return CreateToken(foundUser);
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}