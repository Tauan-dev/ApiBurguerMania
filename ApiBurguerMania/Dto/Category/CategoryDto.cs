using ApiBurguerMania.Models;

namespace ApiBurguerMania.Dto.Category
{
    public class CategoryDTO
    {
        public int Id { get; set; }  
        public string Name { get; set; }
        public string Description { get; set; }
        public string PathImage { get; set; }
    }
}
