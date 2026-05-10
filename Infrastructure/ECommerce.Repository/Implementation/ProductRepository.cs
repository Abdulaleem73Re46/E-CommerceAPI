using Core.Contracts;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Repository;
using SQLitePCL;

namespace Repository;

public class ProductRepository : RepositoryBase<Product>, IProductRepository
{
    public ProductRepository(RepositoryContext repository) : base(repository)
    {
    }

    public void CreateProduct(Product Product)=>Create(Product);


    public void DeleteProduct(Product Product)
    {
       Delete(Product);
    }

    public async Task<Product> GetProductAsync(Guid ProductId, bool trackChanges)
    {
      return await  FindByCondition(p=>p.ProductId.Equals(ProductId),trackChanges)
      .SingleOrDefaultAsync();    
    }

    public async Task<IEnumerable<Product>> GetProductsAsync(bool trackChanges)
    {
        return await FindAll(trackChanges).OrderBy(p=>p.Name).ToListAsync();
    }

    public void UpdateProduct(Product Product)
    {
       Update(Product);
    }
}