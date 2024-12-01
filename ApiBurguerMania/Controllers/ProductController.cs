using ApiBurguerMania.Dto.Product;
using ApiBurguerMania.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
                return Ok(product);
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
                return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.Id }, createdProduct); // Correção no nome da propriedade de retorno
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
                var success = await _productService.UpdateProductAsync(id, productDto);
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
    }
}
