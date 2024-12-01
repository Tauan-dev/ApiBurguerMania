using ApiBurguerMania.Models;

namespace ApiBurguerMania.Dto.Order
{
    public class OrderDTO
    {
        public int Id { get; set; }  // Alterado de OrderId para Id
        public int StatusId { get; set; }  // Correto como StatusId
        public decimal Value { get; set; }  // Correto, sem alteração
        public int UserId { get; set; }  // Correto, sem alteração
    }
}
