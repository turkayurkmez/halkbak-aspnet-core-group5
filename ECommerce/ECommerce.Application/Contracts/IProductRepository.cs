using System;
using ECommerce.Domain;

namespace ECommerce.Application.Contracts;

public interface IProductRepository : IRepository<Product>
{
    Task<IReadOnlyList<Product>> GetProductsByCategory(int id);
    Task<IReadOnlyList<Product>> GetProductsByPrice(decimal min, decimal max);  
}
