using System.ComponentModel.DataAnnotations;

namespace ApiBurguerMania.Dto.Category
{
    public class UpdateCategoryDTO
    {
        [Required(ErrorMessage = "O nome da categoria é obrigatório.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "A descrição da categoria é obrigatória.")]
        public string Description { get; set; }

        [Url(ErrorMessage = "A URL da imagem não é válida.")]
        public string PathImage { get; set; }
    }
}
