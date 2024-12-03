using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ApiBurguerMania.Data;
using ApiBurguerMania.Service.Interface;
using ApiBurguerMania.Dto.Product;
using ApiBurguerMania.Models;
using System;

namespace ApiBurguerMania.Service
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Obter todos os produtos
        public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
        {
            return await _dbContext.Products
                .Select(product => new ProductDTO
                {
                    Id = product.Id,
                    Name = product.Name,
                    BaseDescription = product.BaseDescription,
                    FullDescription = product.FullDescription,
                    Price = product.Price,
                    CategoryId = product.CategoryId
                })
                .ToListAsync();
        }

        // Obter um produto por ID
        public async Task<ProductDTO> GetProductByIdAsync(int productId)
        {
            var product = await _dbContext.Products.FindAsync(productId);
            if (product == null)
            {
                throw new KeyNotFoundException("Produto não encontrado");
            }

            return new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                BaseDescription = product.BaseDescription,
                FullDescription = product.FullDescription,
                Price = product.Price,
                CategoryId = product.CategoryId
            };
        }

        // Criar um novo produto
        public async Task<ProductDTO> CreateProductAsync(CreateProductDTO productDto)
        {
            if (productDto == null)
            {
                throw new ArgumentNullException(nameof(productDto), "Os dados do produto não podem ser nulos.");
            }

            if (string.IsNullOrWhiteSpace(productDto.Name))
            {
                throw new ArgumentException("O nome do produto não pode estar vazio.", nameof(productDto.Name));
            }

            if (productDto.Price <= 0)
            {
                throw new ArgumentException("O preço do produto deve ser maior que zero.", nameof(productDto.Price));
            }

            // Criando o objeto Product
            var product = new Product
            {
                Name = productDto.Name,
                BaseDescription = productDto.BaseDescription!, // Usando BaseDescription
                FullDescription = productDto.FullDescription!, // Usando FullDescription
                Price = productDto.Price,
                CategoryId = productDto.CategoryId,
                PathImage = productDto.PathImage! // Usando PathImage
            };

            // Adicionando ao contexto do EF
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();

            // Retornando o DTO com os dados do produto criado
            return new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                PathImage = product.PathImage,
                Price = product.Price,
                BaseDescription = product.BaseDescription,
                FullDescription = product.FullDescription,
                CategoryId = product.CategoryId
            };
        }

        // Atualizar um produto existente
        public async Task<bool> UpdateProductAsync(int productId, UpdateProductDTO productDto)
        {
            try
            {
                var product = await _dbContext.Products.FindAsync(productId);

                if (product == null)
                {
                    return false;
                }

                product.Name = productDto.Name!;
                product.PathImage = productDto.PathImage!;
                product.Price = productDto.Price;
                product.BaseDescription = productDto.BaseDescription!;
                product.FullDescription = productDto.FullDescription!;

                _dbContext.Entry(product).State = EntityState.Modified;

                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                // Log a exception and return false
                Console.WriteLine($"Erro ao atualizar o produto: {ex.Message}");
                return false;
            }
        }






        // Deletar um produto
        public async Task<bool> DeleteProductAsync(int productId)
        {
            var product = await _dbContext.Products.FindAsync(productId);
            if (product == null)
            {
                throw new KeyNotFoundException("Produto não encontrado");
            }

            // Remover produto do contexto
            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        // Novo método para buscar produtos por categoria
        public async Task<IEnumerable<ProductDTO>> GetProductsByCategoryAsync(int categoryId)
        {
            var products = await _dbContext.Products
                .Where(p => p.CategoryId == categoryId) // Agora buscando diretamente pelo categoryId
                .Select(product => new ProductDTO
                {
                    Id = product.Id,
                    Name = product.Name,
                    BaseDescription = product.BaseDescription,
                    FullDescription = product.FullDescription,
                    Price = product.Price,
                    CategoryId = product.CategoryId
                })
                .ToListAsync();

            if (products == null || !products.Any())
            {
                return Enumerable.Empty<ProductDTO>(); // Retorna uma lista vazia se nenhum produto for encontrado
            }

            return products;
        }

    }
}