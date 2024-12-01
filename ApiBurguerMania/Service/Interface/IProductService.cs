using ApiBurguerMania.Dto.Product;

namespace ApiBurguerMania.Service.Interface
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetAllProductsAsync();
        Task<ProductDTO> GetProductByIdAsync(int productId);
        Task<ProductDTO> CreateProductAsync(CreateProductDTO productDto);
        Task<bool> UpdateProductAsync(int productId, UpdateProductDTO productDto);
        Task<bool> DeleteProductAsync(int productId);
    }
}
