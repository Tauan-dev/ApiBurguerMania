using ApiBurguerMania.Dto.Category;

namespace ApiBurguerMania.Service.Interface
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync();
        Task<CategoryDTO> GetCategoryByIdAsync(int categoryId);
        Task<CategoryDTO> CreateCategoryAsync(CreateCategoryDTO categoryDto);
        Task<bool> UpdateCategoryAsync(int categoryId, UpdateCategoryDTO categoryDto);
        Task<bool> DeleteCategoryAsync(int categoryId);
    }
}
