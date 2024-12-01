using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ApiBurguerMania.Data;
using ApiBurguerMania.Dto.Order;
using ApiBurguerMania.Models;
using ApiBurguerMania.Service.Interface;

namespace ApiBurguerMania.Service
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _dbContext;

        public OrderService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Obter todos os pedidos
        public async Task<IEnumerable<OrderDTO>> GetAllOrdersAsync()
        {
            return await _dbContext.Orders
                .Select(order => new OrderDTO
                {
                    Id = order.Id,  // Usando "Id" conforme o modelo de Order
                    StatusId = order.StatusId,  // Usando "StatusId" diretamente
                    UserId = order.UserId,  // Usando "UserId" diretamente
                    Value = order.Value  // Valor do pedido
                })
                .ToListAsync();
        }

        // Obter um pedido por ID
        public async Task<OrderDTO> GetOrderByIdAsync(int orderId)
        {
            var order = await _dbContext.Orders
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
            {
                throw new KeyNotFoundException("Pedido não encontrado");
            }

            return new OrderDTO
            {
                Id = order.Id,  // Usando "Id" conforme o modelo de Order
                StatusId = order.StatusId,  // Usando "StatusId" diretamente
                UserId = order.UserId,  // Usando "UserId" diretamente
                Value = order.Value  // Valor do pedido
            };
        }

        // Criar um novo pedido
        public async Task<OrderDTO> CreateOrderAsync(int userId, CreateOrderDTO orderDto)
        {
            var order = new Order
            {
                UserId = userId,
                StatusId = orderDto.StatusId,
                Value = orderDto.Value  // Usando "Value" em vez de "TotalValue"
            };

            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();

            return new OrderDTO
            {
                Id = order.Id,  // Usando "Id" conforme o modelo de Order
                StatusId = order.StatusId,
                UserId = order.UserId,
                Value = order.Value  // Usando "Value" em vez de "TotalValue"
            };
        }

        // Atualizar um pedido existente
        public async Task<bool> UpdateOrderAsync(int orderId, UpdateOrderDTO orderDto)
        {
            var order = await _dbContext.Orders.FindAsync(orderId);
            if (order == null)
            {
                throw new KeyNotFoundException("Pedido não encontrado");
            }

            order.StatusId = orderDto.StatusId;
            order.Value = orderDto.Value;  // Usando "Value" em vez de "TotalValue"

            _dbContext.Entry(order).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return true;
        }

        // Deletar um pedido
        public async Task<bool> DeleteOrderAsync(int orderId)
        {
            var order = await _dbContext.Orders.FindAsync(orderId);
            if (order == null)
            {
                throw new KeyNotFoundException("Pedido não encontrado");
            }

            _dbContext.Orders.Remove(order);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
