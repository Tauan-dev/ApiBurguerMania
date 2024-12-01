using System.ComponentModel.DataAnnotations;

namespace ApiBurguerMania.Dto.Status
{
    public class UpdateStatusDTO
    {
        [Required(ErrorMessage = "O nome do status é obrigatório.")]
        public string Name { get; set; }
    }
}
