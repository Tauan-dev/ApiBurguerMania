using ApiBurguerMania.Dto.Order;
using ApiBurguerMania.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiBurguerMania.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        // Injeção de dependência
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // Obter todos os pedidos
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            try
            {
                var orders = await _orderService.GetAllOrdersAsync();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                // Caso haja um erro interno no servidor
                return StatusCode(500, $"Erro interno no servidor: {ex.Message}");
            }
        }

        // Obter um pedido por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            try
            {
                var order = await _orderService.GetOrderByIdAsync(id);
                return Ok(order);
            }
            catch (KeyNotFoundException)
            {
                // Caso o pedido não seja encontrado
                return NotFound("Pedido não encontrado");
            }
            catch (Exception ex)
            {
                // Caso haja um erro interno no servidor
                return StatusCode(500, $"Erro interno no servidor: {ex.Message}");
            }
        }

        // Criar um novo pedido
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDTO orderDto)
        {
            if (orderDto == null)
            {
                // Caso os dados do pedido sejam nulos
                return BadRequest("Os dados do pedido são obrigatórios.");
            }

            try
            {
                // Obtenção do userId do DTO
                var userId = orderDto.UserId;

                // Criando o pedido usando o serviço
                var createdOrder = await _orderService.CreateOrderAsync(userId, orderDto);

                // Retorno com status 201 e a URL do novo recurso
                return CreatedAtAction(nameof(GetOrderById), new { id = createdOrder.Id }, createdOrder);
            }
            catch (Exception ex)
            {
                // Caso haja um erro interno no servidor
                return StatusCode(500, $"Erro interno no servidor: {ex.Message}");
            }
        }

        // Atualizar um pedido existente
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] UpdateOrderDTO orderDto)
        {
            if (orderDto == null)
            {
                // Caso os dados do pedido sejam nulos
                return BadRequest("Os dados do pedido são obrigatórios.");
            }

            try
            {
                var success = await _orderService.UpdateOrderAsync(id, orderDto);
                if (success)
                {
                    // Sucesso, sem conteúdo a retornar
                    return NoContent();
                }

                // Caso o pedido não seja encontrado
                return NotFound("Pedido não encontrado");
            }
            catch (KeyNotFoundException)
            {
                // Caso o pedido não seja encontrado
                return NotFound("Pedido não encontrado");
            }
            catch (Exception ex)
            {
                // Caso haja um erro interno no servidor
                return StatusCode(500, $"Erro interno no servidor: {ex.Message}");
            }
        }

        // Deletar um pedido
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            try
            {
                var success = await _orderService.DeleteOrderAsync(id);
                if (success)
                {
                    // Sucesso, sem conteúdo a retornar
                    return NoContent();
                }

                // Caso o pedido não seja encontrado
                return NotFound("Pedido não encontrado");
            }
            catch (KeyNotFoundException)
            {
                // Caso o pedido não seja encontrado
                return NotFound("Pedido não encontrado");
            }
            catch (Exception ex)
            {
                // Caso haja um erro interno no servidor
                return StatusCode(500, $"Erro interno no servidor: {ex.Message}");
            }
        }
    }
}
