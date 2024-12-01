namespace ApiBurguerMania.Models
{
    public class Product
    {
        // Renomeado para manter consistência com outros modelos
        public int Id { get; set; }

        public string Name { get; set; }
        public string PathImage { get; set; }
        public decimal Price { get; set; }
        public string BaseDescription { get; set; }
        public string FullDescription { get; set; }

        // Relacionamento 1:N com Category
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        // Relacionamento N:N com Orders (via OrderProduct)
        public ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
