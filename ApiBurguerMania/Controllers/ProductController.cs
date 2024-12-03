using ApiBurguerMania.Dto.Product;
using ApiBurguerMania.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiBurguerMania.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        // Injeção de dependência
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // Obter todos os produtos
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var products = await _productService.GetAllProductsAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                // Retorna erro interno no servidor
                return StatusCode(500, $"Erro interno no servidor: {ex.Message}");
            }
        }

        // Obter um produto por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                if (product == null)
                {
                    return NotFound("Produto não encontrado");
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                // Retorna erro interno no servidor
                return StatusCode(500, $"Erro interno no servidor: {ex.Message}");
            }
        }

        // Criar um novo produto
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDTO productDto)
        {
            if (productDto == null)
            {
                // Caso os dados sejam nulos
                return BadRequest("Os dados do produto são obrigatórios.");
            }

            try
            {
                var createdProduct = await _productService.CreateProductAsync(productDto);
                return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.Id }, createdProduct);
            }
            catch (Exception ex)
            {
                // Retorna erro interno no servidor
                return StatusCode(500, $"Erro interno no servidor: {ex.Message}");
            }
        }

        // Atualizar um produto existente
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductDTO productDto)
        {
            if (productDto == null)
            {
                // Caso os dados sejam nulos
                return BadRequest("Os dados do produto são obrigatórios.");
            }

            try
            {
                // Verifica se o produto existe antes de tentar atualizar
                var existingProduct = await _productService.GetProductByIdAsync(id);
                if (existingProduct == null)
                {
                    return NotFound("Produto não encontrado");
                }

                // Realiza a atualização
                var updatedProduct = await _productService.UpdateProductAsync(id, productDto);
                if (updatedProduct == null)
                {
                    return BadRequest("Erro ao atualizar o produto.");
                }

                return Ok(updatedProduct); // Retorna o produto atualizado
            }
            catch (Exception ex)
            {
                // Retorna erro interno no servidor
                return StatusCode(500, $"Erro interno no servidor: {ex.Message}");
            }
        }

        // Deletar um produto
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var success = await _productService.DeleteProductAsync(id);
                if (success)
                {
                    return NoContent(); // Sucesso, sem conteúdo a retornar
                }

                // Caso o produto não seja encontrado
                return NotFound("Produto não encontrado");
            }
            catch (KeyNotFoundException)
            {
                // Caso o produto não seja encontrado
                return NotFound("Produto não encontrado");
            }
            catch (Exception ex)
            {
                // Retorna erro interno no servidor
                return StatusCode(500, $"Erro interno no servidor: {ex.Message}");
            }
        }

        // Novo método para buscar produtos por categoria
        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetProductsByCategory(int categoryId)
        {
            try
            {
                var products = await _productService.GetProductsByCategoryAsync(categoryId);
                if (products == null || !products.Any())
                {
                    return NotFound("Nenhum produto encontrado para esta categoria.");
                }

                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno no servidor: {ex.Message}");
            }
        }
    }
}
