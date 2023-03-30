﻿namespace CleanOnionExample.Data.Entities.Services;

public interface IPersonRepository {
  IQueryable<Person> Person { get; }
  //Task<List<Product>> GetListAsync();
  //Task<Product> GetByIdAsync(int productId);
  //Task<int> InsertAsync(Product product);
  //Task UpdateAsync(Product product);
  //Task DeleteAsync(Product product);
}