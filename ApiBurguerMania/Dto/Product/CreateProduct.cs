using System.ComponentModel.DataAnnotations;

namespace ApiBurguerMania.Dto.Product
{
    public class CreateProductDTO
    {

        public string? Name { get; set; }
        public string? PathImage { get; set; }
        public decimal Price { get; set; }
        public string? BaseDescription { get; set; }
        public string? FullDescription { get; set; }
        public int CategoryId { get; set; }

    }
}
