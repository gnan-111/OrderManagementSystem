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

            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var key = System.Text.Encoding.UTF8.GetBytes(_config["JwtConfig:Key"]);
            var claims = new List<System.Security.Claims.Claim>
            {
                new(System.Security.Claims.ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(System.Security.Claims.ClaimTypes.Email, user.Email),
                new(System.Security.Claims.ClaimTypes.Role, user.Role.ToString())
            };
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["JwtConfig:TokenValidityMins"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _config["JwtConfig:Issuer"],
                Audience = _config["JwtConfig:Audience"]
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }

}