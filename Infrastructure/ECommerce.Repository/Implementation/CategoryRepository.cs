using Core.Contracts;
using Core.Entities;
using Core.Shared.Features;
using Microsoft.EntityFrameworkCore;

namespace Repository;



public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
{
    public CategoryRepository(RepositoryContext repository) : base(repository)
    {
    }

    public void CreateCategory(Category Category)=>Create(Category);

    public void DeleteCategory(Category Category)=>Delete(Category);

    public async Task<Category> GetCategoryAsync(Guid CategoryId, bool trackChanges)=>await FindByCondition(ca=>ca.CategoryId.Equals(CategoryId),trackChanges)
    .SingleOrDefaultAsync();

    public async Task<IEnumerable<Category>> GetCategoriesAsync(CategoryParameter categoryParameter,bool trackChanges)=>await FindAll(trackChanges)
    .OrderBy(ca=>ca.Name).Skip((categoryParameter.PageNumber-1)* categoryParameter.PageSize).Take(categoryParameter.PageSize)
    .ToListAsync();
    public void UpdateCategory(Category Category)=>Update(Category);

    
    public async Task<Category?> GetCategoryWithProductsAsync(Guid categoryId)
    {
        return await FindByCondition(c=>c.CategoryId.Equals(categoryId),false).Include(p=>p.Products).SingleOrDefaultAsync();
    }

    //public async Task<IEnumerable<Product?>> GetProductsAsync(Guid CategoryId, bool trackChanges)=>await FindByCondition(c=>c.CategoryId.Equals(CategoryId),trackChanges).Include(p=>p.Products).ToListAsync();
   
}