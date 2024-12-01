namespace ApiBurguerMania.Models
{
    public class User
    {
        // Renomeado para manter consistência com outros modelos
        public int Id { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        // Relacionamento N:N com Orders
        public ICollection<Order> Orders { get; set; }
    }
}
