using OrderManagementSystem.API.Models;

namespace OrderManagementSystem.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByEmailAsync(string email);
        Task AddAsync(User user);
    }
}
