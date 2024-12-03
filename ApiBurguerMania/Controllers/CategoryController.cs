using ApiBurguerMania.Dto.Category;
using ApiBurguerMania.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ApiBurguerMania.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        // Injeção de dependência
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // Obter todas as categorias
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var categories = await _categoryService.GetAllCategoriesAsync();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                // Retorna erro interno no servidor
                return StatusCode(500, $"Erro interno no servidor: {ex.Message}");
            }
        }

        // Obter uma categoria por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            try
            {
                var category = await _categoryService.GetCategoryByIdAsync(id);
                if (category == null)
                {
                    return NotFound("Categoria não encontrada");
                }
                return Ok(category);
            }
            catch (Exception ex)
            {
                // Retorna erro interno no servidor
                return StatusCode(500, $"Erro interno no servidor: {ex.Message}");
            }
        }

        // Criar uma nova categoria
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDTO categoryDto)
        {
            if (categoryDto == null)
            {
                return BadRequest("Os dados da categoria são obrigatórios.");
            }

            try
            {
                // Aceitar o PathImage como uma string simples
                var createdCategory = await _categoryService.CreateCategoryAsync(categoryDto);
                return CreatedAtAction(nameof(GetCategoryById), new { id = createdCategory.Id }, createdCategory);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno no servidor: {ex.Message}");
            }
        }

        // Atualizar uma categoria existente
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] UpdateCategoryDTO categoryDto)
        {
            if (categoryDto == null)
            {
                return BadRequest("Os dados da categoria são obrigatórios.");
            }

            try
            {
                var success = await _categoryService.UpdateCategoryAsync(id, categoryDto);
                if (!success)
                {
                    return NotFound("Categoria não encontrada");
                }
                return NoContent(); // Sucesso, sem conteúdo a retornar
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno no servidor: {ex.Message}");
            }
        }

        // Deletar uma categoria
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                var success = await _categoryService.DeleteCategoryAsync(id);
                if (!success)
                {
                    return NotFound("Categoria não encontrada");
                }
                return NoContent(); // Sucesso, sem conteúdo a retornar
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno no servidor: {ex.Message}");
            }
        }
    }
}
