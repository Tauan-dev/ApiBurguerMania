using System.ComponentModel.DataAnnotations;

namespace ApiBurguerMania.Dto.Category
{
    public class CreateCategoryDTO
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string PathImage { get; set; }  // Renomeado de ImagePath para PathImage
    }
}
