using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ApiBurguerMania.Data;
using ApiBurguerMania.Dto.Category;
using ApiBurguerMania.Models;
using ApiBurguerMania.Service.Interface;

namespace ApiBurguerMania.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Obter todas as categorias
        public async Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync()
        {
            return await _dbContext.Categories
                .Select(category => new CategoryDTO
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description,
                    PathImage = category.PathImage
                })
                .ToListAsync();
        }

        // Obter uma categoria por ID
        public async Task<CategoryDTO> GetCategoryByIdAsync(int categoryId)
        {
            var category = await _dbContext.Categories
                .FirstOrDefaultAsync(c => c.Id == categoryId);

            if (category == null)
            {
                throw new KeyNotFoundException("Categoria não encontrada");
            }

            return new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                PathImage = category.PathImage
            };
        }

        // Criar uma nova categoria
        public async Task<CategoryDTO> CreateCategoryAsync(CreateCategoryDTO categoryDto)
        {
            if (categoryDto == null)
            {
                throw new ArgumentNullException(nameof(categoryDto), "Os dados da categoria não podem ser nulos.");
            }

            // Não estamos validando o campo PathImage, ele pode ser qualquer string
            var category = new Category
            {
                Name = categoryDto.Name,
                Description = categoryDto.Description,
                PathImage = categoryDto.PathImage
            };

            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();

            return new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                PathImage = category.PathImage
            };
        }

        // Atualizar uma categoria existente
        public async Task<bool> UpdateCategoryAsync(int categoryId, UpdateCategoryDTO categoryDto)
        {
            var category = await _dbContext.Categories
                .FirstOrDefaultAsync(c => c.Id == categoryId);

            if (category == null)
            {
                throw new KeyNotFoundException("Categoria não encontrada");
            }

            // Não estamos validando o campo PathImage, ele pode ser qualquer string
            category.Name = categoryDto.Name;
            category.Description = categoryDto.Description;
            category.PathImage = categoryDto.PathImage;

            _dbContext.Entry(category).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return true;
        }

        // Deletar uma categoria
        public async Task<bool> DeleteCategoryAsync(int categoryId)
        {
            var category = await _dbContext.Categories
                .FirstOrDefaultAsync(c => c.Id == categoryId);

            if (category == null)
            {
                throw new KeyNotFoundException("Categoria não encontrada");
            }

            _dbContext.Categories.Remove(category);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
