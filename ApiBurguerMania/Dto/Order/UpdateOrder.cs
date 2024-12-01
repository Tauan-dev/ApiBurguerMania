using System;
using System.ComponentModel.DataAnnotations;

namespace ApiBurguerMania.Dto.Order
{
    public class UpdateOrderDTO
    {
        [Required(ErrorMessage = "O status do pedido é obrigatório.")]
        public int StatusId { get; set; }

        [Required(ErrorMessage = "O valor do pedido é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor do pedido deve ser maior que zero.")]
        public decimal Value { get; set; }

        // A data de atualização pode ser manipulada na camada de serviço
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow; // Data de atualização automática
    }
}
