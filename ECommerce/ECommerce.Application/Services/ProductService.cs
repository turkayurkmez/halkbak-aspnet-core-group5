using ECommerce.Application.Contracts;
using ECommerce.Domain;

namespace ECommerce.Application.Services;

public class ProductService(IProductRepository productRepository) : IProductService
{
   

    public async Task<IReadOnlyList<Product>> GetProductsAsync()
    {
        return await productRepository.ListAsync();
       
    }

    public async Task<Product?> GetProductAsync(int id)
    {
        
        return await productRepository.GetByIdAsync(id);
    }

    public async Task<Product> Create(Product product)
    {
        await productRepository.AddAsync(product);
        return product;
    }




}
