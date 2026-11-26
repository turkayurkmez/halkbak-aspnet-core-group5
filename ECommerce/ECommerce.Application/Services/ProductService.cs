using ECommerce.Domain;

namespace ECommerce.Application.Services;

public class ProductService : IProductService
{
    //amacımız db olmadığı için koleksiyon bellekte:
    private readonly List<Product> products = new()
    {
         new Product{ Id=1, Name="Ürün A", Price=100, CategoryId=1, Sku="Test-001", Stock=100},
         new Product{ Id=2, Name="Ürün B", Price=100, CategoryId=1, Sku="Test-002", Stock=100},
         new Product{ Id=3, Name="Ürün C", Price=100, CategoryId=1, Sku="Test-003", Stock=100}

    };

    public async Task<IReadOnlyList<Product>> GetProductsAsync()
    {
        await Task.CompletedTask;
        return products.ToList();
    }

    public async Task<Product?> GetProductAsync(int id)
    {
        await Task.CompletedTask;
        return products.FirstOrDefault(p => p.Id == id);
    }

    public async Task<Product> Create(Product product)
    {
        await Task.CompletedTask;
        products.Add(product);
        return product;
    }




}
