namespace OrderManagementSystem.DTOs
{
    public class OrderCreateDTO
    {
        public string CustomerName { get; set; }
        public string Product { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
    }
}
