using System;
using OrderManagementSystem.API.Models;

namespace OrderManagementSystem.DTOs
{
    public class UserRegiDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public RoleEnum Role { get; set; } // "Admin" or "User"
    }
}
