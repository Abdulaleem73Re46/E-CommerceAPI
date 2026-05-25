using Core.Contracts;
using Core.Entities;
using Core.Shared.Features;
using Microsoft.EntityFrameworkCore;
using Repository;
using SQLitePCL;

namespace Repository;

public class ProductRepository : RepositoryBase<Product>, IProductRepository
{ 
    RepositoryContext _repo;
    public ProductRepository(RepositoryContext repository) : base(repository)
    {
        _repo=repository;
    }

  
    public void CreateProduct(Product Product){
         Console.WriteLine("inside repo....");
        Create(Product);
        }


    public void DeleteProduct(Product Product)
    {
       Delete(Product);
    }

    public async  Task DeleteProductsByCategoryId(Guid Id)
    {

        var pros=await FindByCondition(p=>p.CategoryId.Equals(Id),false).ToListAsync();
         _repo.RemoveRange(pros);
         await _repo.SaveChangesAsync();        
  

    }

    public async Task<Product> GetProductAsync(Guid ProductId, bool trackChanges)
    {
      return await  FindByCondition(p=>p.ProductId.Equals(ProductId),trackChanges)
      .SingleOrDefaultAsync();    
    }

    public async Task<IEnumerable<Product>> GetProductsAsync(Guid id,ProductParameter productParameters,bool trackChanges)
    {
        return await FindAll(trackChanges).OrderBy(p=>p.Name).Skip((productParameters.PageNumber-1)* productParameters.PageSize).Take(productParameters.PageSize).ToListAsync();

    }

    public void UpdateProduct(Product Product)
    {
       Update(Product);
    }

  public async Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(Guid Id,ProductParameter productParameters,bool trackChanges)
    {
        var prods=await FindByCondition(c=>c.CategoryId.Equals(Id),trackChanges).OrderBy(p=>p.Name).Skip((productParameters.PageNumber-1)*productParameters.PageSize).Take(productParameters.PageSize).ToListAsync();
  return prods;

    }

}