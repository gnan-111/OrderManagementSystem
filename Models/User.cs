using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderManagementSystem.API.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string Email { get; set; }
        public required byte[] PasswordHash { get; set; }
        public required byte[] PasswordSalt { get; set; }

        [Required]
        public RoleEnum Role { get; set; } // presetted the values as enum to accept "Admin" or "User" only
    }
}