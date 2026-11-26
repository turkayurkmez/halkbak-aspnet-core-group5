using System;

namespace ECommerce.Application.Contracts;

public interface IRepository<T> where T:class, new()
{
   //Tüm db operasyonlarının kalıbı burada:
   Task<IReadOnlyList<T>> ListAsync();
   Task<T?> GetByIdAsync(int id);

   Task AddAsync(T entity);
   void Update(T entity);
   void Remove(T entity);

   Task<bool> IsExistsAsync(int id);

}
