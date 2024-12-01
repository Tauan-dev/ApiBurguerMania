using System.ComponentModel.DataAnnotations;

namespace ApiBurguerMania.Dto.Product
{
    public class CreateProductDTO
    {
        [Required(ErrorMessage = "O nome do produto é obrigatório.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "A descrição básica do produto é obrigatória.")]
        public string BaseDescription { get; set; }

        [Required(ErrorMessage = "A descrição completa do produto é obrigatória.")]
        public string FullDescription { get; set; }

        [Required(ErrorMessage = "O preço do produto é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "A categoria do produto é obrigatória.")]
        public int CategoryId { get; set; }

        [Url(ErrorMessage = "A URL da imagem não é válida.")]
        public string PathImage { get; set; }
    }
}
