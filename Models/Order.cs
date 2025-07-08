using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderManagementSystem.API.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public required string CustomerName { get; set; }
        public string Product { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; } // custom status to order like shipped deliverd or improcess
        public DateTime CreatedAt { get; set; }
    }
}
