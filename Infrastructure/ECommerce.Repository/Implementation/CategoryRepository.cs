using Core.Contracts;
using Core.Entities;
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

    public async Task<IEnumerable<Category>> GetCategorysAsync(bool trackChanges)=>await FindAll(trackChanges)
    .OrderBy(ca=>ca.Name)
    .ToListAsync();
    public void UpdateCategory(Category Category)=>Update(Category);

    public async Task<Category?> GetCategoryWithProductsAsync(Guid categoryId)
    {
        return await FindByCondition(c=>c.CategoryId.Equals(categoryId),false).Include(p=>p.Products).SingleOrDefaultAsync();
    }

   




}