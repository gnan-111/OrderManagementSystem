using AutoMapper;
using OrderManagementSystem.API.Models;
using OrderManagementSystem.DTOs;
using OrderManagementSystem.Repositories;
using System;

namespace OrderManagementSystem.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repo;
        private readonly IMapper _mapper;
        public OrderService(IOrderRepository repo, IMapper mapper)
        {
            _repo = repo; _mapper = mapper;
        }
        public async Task<IEnumerable<OrderDTO>> GetAllAsync()
        {
            var orders = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<OrderDTO>>(orders);
        }
        public async Task<OrderDTO> GetByIdAsync(int id)
        {
            var order = await _repo.GetByIdAsync(id) ?? throw new Exception("Order not found");
            return _mapper.Map<OrderDTO>(order);
        }
        public async Task<OrderDTO> CreateAsync(OrderCreateDTO dto)
        {
            var order = _mapper.Map<Order>(dto);
            order.CreatedAt = DateTime.UtcNow;
            await _repo.AddAsync(order);
            return _mapper.Map<OrderDTO>(order);
        }
        public async Task<OrderDTO> UpdateAsync(int id, OrderCreateDTO dto)
        {
            var order = await _repo.GetByIdAsync(id) ?? throw new Exception("Order not found");
            order.CustomerName = dto.CustomerName;
            order.Product = dto.Product;
            order.Amount = dto.Amount;
            order.Status = dto.Status;
            await _repo.UpdateAsync(order);
            return _mapper.Map<OrderDTO>(order);
        }
        public async Task DeleteAsync(int id)
        {
            var order = await _repo.GetByIdAsync(id) ?? throw new Exception("Order not found");
            await _repo.DeleteAsync(order);
        }

        //Task<IEnumerable<OrderDTO>> IOrderService.GetAllAsync()
        //{
        //    throw new NotImplementedException();
        //}

        Task<OrderDTO> IOrderService.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        //public Task<OrderDTO> CreateAsync(OrderCreateDTO dto)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<OrderDTO> UpdateAsync(int id, OrderCreateDTO dto);
        //}
    }
}
