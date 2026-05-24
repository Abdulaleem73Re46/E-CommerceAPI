using Core.Entities;
using Core.Shared.DataTransferObjects;
using Core.Shared.Features;
namespace Service.Contracts;




public interface ICategoryService
{
    Task<IEnumerable<CategoryDto>> GetCategoriesAsync(CategoryParameter categoryParameter,bool trackChanges);
    Task<CategoryDto?> CategoryAsync(Guid CategoryId,bool trackChanges);
    
    Task<IEnumerable<ProductDto?>> GetProductsDtosAsync(Guid CategoryId,bool trackChanges);

    Task<CategoryDto> CreateCategoryDtoAsync(CategoryForCreationDto categoryForCreation,bool trackChanges);

    Task UpdateCategory(CategoryForUpdateDto categoryForUpdateDto);

Task<bool> DeleteCategoryAsync(Guid categoryId);
    


    
}
