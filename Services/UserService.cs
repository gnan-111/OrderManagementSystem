using Microsoft.IdentityModel.Tokens;
using OrderManagementSystem.API.Models;
using OrderManagementSystem.DTOs;
using OrderManagementSystem.Repositories;

namespace OrderManagementSystem.Services
{
    public class UserService : IUserService
    {
        private const string V = "id";
        private readonly IUserRepository _repo;
        private readonly IConfiguration _config;
        public UserService(IUserRepository repo, IConfiguration config)
        {
            _repo = repo; _config = config;
        }
        public async Task RegisterAsync(UserRegiDTO dto)
        {
            if (await _repo.GetByEmailAsync(dto.Email) != null)
                throw new Exception("Email already registered");
            using var hmac = new System.Security.Cryptography.HMACSHA512();
            var user = new User
            {
                Email = dto.Email,
                PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(dto.Password)),
                PasswordSalt = hmac.Key,
                Role = dto.Role
            };
            await _repo.AddAsync(user);
        }
        public async Task<string> LoginAsync(UserLoginDTO dto)
        {
            var user = await _repo.GetByEmailAsync(dto.Email) ?? throw new Exception("Invalid credentials");
            using var hmac = new System.Security.Cryptography.HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(dto.Password));
            if (!computedHash.SequenceEqual(user.PasswordHash))
                throw new Exception("Invalid credentials");
            // Generate JWT
            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var key = System.Text.Encoding.ASCII.GetBytes(_config["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]{
                new System.Security.Claims.Claim(V, user.Id.ToString()),
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, user.Role.ToString())

            }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
