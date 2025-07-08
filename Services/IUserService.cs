using Microsoft.IdentityModel.Tokens;
using OrderManagementSystem.API.Models;
using OrderManagementSystem.DTOs;
using System;

namespace OrderManagementSystem.Services
{
    public interface IUserService
    {
        Task RegisterAsync(UserRegiDTO dto);
        Task<string> LoginAsync(UserLoginDTO dto);
    }

}
