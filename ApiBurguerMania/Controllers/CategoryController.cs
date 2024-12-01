using ApiBurguerMania.Dto.Category;
using ApiBurguerMania.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
                return Ok(category);
            }
            catch (KeyNotFoundException)
            {
                // Caso a categoria não seja encontrada
                return NotFound("Categoria não encontrada");
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
                // Caso os dados sejam nulos
                return BadRequest("Os dados da categoria são obrigatórios.");
            }

            try
            {
                var createdCategory = await _categoryService.CreateCategoryAsync(categoryDto);
                return CreatedAtAction(nameof(GetCategoryById), new { id = createdCategory.Id }, createdCategory);
            }
            catch (ArgumentNullException ex)
            {
                // Caso os dados sejam inválidos
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // Retorna erro interno no servidor
                return StatusCode(500, $"Erro interno no servidor: {ex.Message}");
            }
        }

        // Atualizar uma categoria existente
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] UpdateCategoryDTO categoryDto)
        {
            if (categoryDto == null)
            {
                // Caso os dados sejam nulos
                return BadRequest("Os dados da categoria são obrigatórios.");
            }

            try
            {
                var success = await _categoryService.UpdateCategoryAsync(id, categoryDto);
                if (success)
                {
                    return NoContent(); // Sucesso, sem conteúdo a retornar
                }

                // Caso a categoria não seja encontrada
                return NotFound("Categoria não encontrada");
            }
            catch (KeyNotFoundException)
            {
                // Caso a categoria não seja encontrada
                return NotFound("Categoria não encontrada");
            }
            catch (Exception ex)
            {
                // Retorna erro interno no servidor
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
                if (success)
                {
                    return NoContent(); // Sucesso, sem conteúdo a retornar
                }

                // Caso a categoria não seja encontrada
                return NotFound("Categoria não encontrada");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Categoria não encontrada");
            }
            catch (Exception ex)
            {
                // Retorna erro interno no servidor
                return StatusCode(500, $"Erro interno no servidor: {ex.Message}");
            }
        }
    }
}
