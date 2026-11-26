using ECommerce.Domain;

namespace ECommerce.Application.Services;

public interface IProductService
{
    Task<Product> Create(Product product);
    Task<Product?> GetProductAsync(int id);
    Task<IReadOnlyList<Product>> GetProductsAsync();
}
