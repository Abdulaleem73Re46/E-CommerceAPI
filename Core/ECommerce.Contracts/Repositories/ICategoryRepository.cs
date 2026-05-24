using Core.Entities;
using Core.Shared.Features;


namespace Core.Contracts;


public interface ICategoryRepository{
  
  
Task<Category> GetCategoryAsync(Guid CategoryId,bool trackChanges);

  Task<IEnumerable<Category>> GetCategoriesAsync(CategoryParameter categoryParameter,bool trackChanges);

  void  CreateCategory(Category Category);
    void  UpdateCategory(Category Category);
    void DeleteCategory(Category Category);

    Task<Category?> GetCategoryWithProductsAsync(Guid categoryId);
  //  Task<IEnumerable<Product?>> GetProductsAsync(Guid CategoryId,bool trackChanges);




}
