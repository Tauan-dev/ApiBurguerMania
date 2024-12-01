namespace ApiBurguerMania.Models
{
    public class Status
    {
        // Renomeado para manter consistência com outros modelos
        public int Id { get; set; }

        public string Name { get; set; }

        // Relacionamento 1:1 com Order
        public Order Order { get; set; }
    }
}
