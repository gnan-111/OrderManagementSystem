using AutoMapper;
using OrderManagementSystem.API.Models;
using OrderManagementSystem.DTOs;
using System;

namespace OrderManagementSystem.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDTO>> GetAllAsync();
        Task<OrderDTO> GetByIdAsync(int id);
        Task<OrderDTO> CreateAsync(OrderCreateDTO dto);
        Task<OrderDTO> UpdateAsync(int id, OrderCreateDTO dto);
        Task DeleteAsync(int id);
    }

}
