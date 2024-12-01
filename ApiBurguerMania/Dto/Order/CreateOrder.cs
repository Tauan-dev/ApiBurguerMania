using System;
using System.ComponentModel.DataAnnotations;

namespace ApiBurguerMania.Dto.Order
{
    public class CreateOrderDTO
    {
        [Required(ErrorMessage = "O status do pedido é obrigatório.")]
        public int StatusId { get; set; }

        [Required(ErrorMessage = "O valor do pedido é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor do pedido deve ser maior que zero.")]
        public decimal Value { get; set; }
        public int UserId { get; set; }  
      
    }
}
