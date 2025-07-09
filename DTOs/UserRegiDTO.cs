using System;
using System.Text.Json.Serialization;
using OrderManagementSystem.API.Models;

namespace OrderManagementSystem.DTOs
{
    public class UserRegiDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }

        //[JsonConverter(typeof(JsonStringEnumConverter))]
        public RoleEnum Role { get; set; } // "Admin" or "User"
    }
}
