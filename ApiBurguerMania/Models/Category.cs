namespace ApiBurguerMania.Models
{
    public class Category
    {
        public int Id { get; set; }  // Alterado para Id
        public string Name { get; set; }
        public string Description { get; set; }
        public string PathImage { get; set; }  // Alterado para ImagePath

        // Relacionamento 1:N com Product
        public ICollection<Product> Products { get; set; }
    }
}
