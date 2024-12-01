using ApiBurguerMania.Dto.Order;

namespace ApiBurguerMania.Service.Interface
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDTO>> GetAllOrdersAsync();
        Task<OrderDTO> GetOrderByIdAsync(int orderId);
        Task<OrderDTO> CreateOrderAsync(int userId, CreateOrderDTO orderDto);
        Task<bool> UpdateOrderAsync(int orderId, UpdateOrderDTO orderDto);
        Task<bool> DeleteOrderAsync(int orderId);
    }
}
