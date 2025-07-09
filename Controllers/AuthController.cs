using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.DTOs;
using OrderManagementSystem.Services;

namespace OrderManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _service;
        public AuthController(IUserService service) => _service = service;

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegiDTO dto)
        {
            await _service.RegisterAsync(dto);
            return Ok(new { message = "Registered successfully" });
        }

     
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDTO dto)
        {
            var token = await _service.LoginAsync(dto);
            return Ok(new { token });
        }
    }
}
