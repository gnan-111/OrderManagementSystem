using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.API.Models;
using OrderManagementSystem.Data;

namespace OrderManagementSystem.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _db;
        public OrderRepository(ApplicationDbContext db) => _db = db;
        public async Task<IEnumerable<Order>> GetAllAsync() => await _db.Orders.ToListAsync();
        public async Task<Order> GetByIdAsync(int id) => await _db.Orders.FindAsync(id);
        public async Task AddAsync(Order order)
        {
            _db.Orders.Add(order);
            await _db.SaveChangesAsync();
        }
        public async Task UpdateAsync(Order order)
        {
            _db.Orders.Update(order);
            await _db.SaveChangesAsync();
        }
        public async Task DeleteAsync(Order order)
        {
            _db.Orders.Remove(order);
            await _db.SaveChangesAsync();
        }
    }
}
