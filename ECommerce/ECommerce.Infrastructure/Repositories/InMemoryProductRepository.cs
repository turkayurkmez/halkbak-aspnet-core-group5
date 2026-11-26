using System;
using ECommerce.Application.Contracts;
using ECommerce.Domain;

namespace ECommerce.Infrastructure.Repositories;

public class InMemoryProductRepository : IProductRepository
{
    public Task AddAsync(Product entity)
    {
        throw new NotImplementedException();
    }

    public Task<Product?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

     //amacımız db olmadığı için koleksiyon bellekte:
    private readonly List<Product> products = new()
    {
         new Product{ Id=1, Name="Ürün A", Price=100, CategoryId=1, Sku="Test-001", Stock=100},
         new Product{ Id=2, Name="Ürün B", Price=100, CategoryId=1, Sku="Test-002", Stock=100},
         new Product{ Id=3, Name="Ürün C", Price=100, CategoryId=1, Sku="Test-003", Stock=100}

    };

    public async Task<IReadOnlyList<Product>> GetProductsByCategory(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<Product>> GetProductsByPrice(decimal min, decimal max)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsExistsAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyList<Product>> ListAsync()
    {
        await Task.CompletedTask;
        return products;  
    }

    public void Remove(Product entity)
    {
        throw new NotImplementedException();
    }

    public void Update(Product entity)
    {
        throw new NotImplementedException();
    }
}
