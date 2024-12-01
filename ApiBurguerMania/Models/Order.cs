namespace ApiBurguerMania.Models
{
    public class Order
    {
        public int Id { get; set; }  // Alterado para Id
        public int StatusId { get; set; }  // Correto como StatusId
        public decimal Value { get; set; }

        // Relacionamento com Status (1:1)
        public Status Status { get; set; }

        // Relacionamento N:N com Produtos
        public ICollection<OrderProduct> OrderProducts { get; set; }

        // Relacionamento N:1 com User
        public int UserId { get; set; }  // Manteve UserId
        public User User { get; set; }
    }
}
