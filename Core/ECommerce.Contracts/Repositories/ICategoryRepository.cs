using Core.Entities;


namespace Core.Contracts;


public interface ICategoryRepository{
  
  
Task<Category> GetCategoryAsync(Guid CategoryId,bool trackChanges);

  Task<IEnumerable<Category>> GetCategorysAsync(bool trackChanges);

  void  CreateCategory(Category Category);
    void  UpdateCategory(Category Category);
    void DeleteCategory(Category Category);

    Task<Category?> GetCategoryWithProductsAsync(Guid categoryId);



}
