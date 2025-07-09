using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.DTOs;
using OrderManagementSystem.Services;

namespace OrderManagementSystem.Controllers
{
    [ApiController]

    [Route("api/[controller]/[action]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _service;
        public OrdersController(IOrderService service) => _service = service;
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpPost]
        public async Task<IActionResult> Create(OrderCreateDTO dto) => Ok(await _service.CreateAsync(dto));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, OrderCreateDTO dto) => Ok(await _service.UpdateAsync(id, dto));


        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return Ok(new { message = "Deleted" });
        }
    }
}
