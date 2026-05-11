using AutoMapper;
using Core.Contracts;
using Core.Shared.DataTransferObjects;
using Service.Contracts;

namespace Service;


internal sealed class CategoryService : ICategoryService
{

    private readonly IRepositoryManager _repository;
private readonly IMapper _mapper;
    public CategoryService(IRepositoryManager repository,IMapper mapper)
    {
        _repository=repository;
        _mapper=mapper;
    }

    public async Task<CategoryDto?> CategoryAsync(Guid CategoryId, bool trackChanges){
       
       var category=await _repository.CategoryRepository.GetCategoryAsync(CategoryId,trackChanges);
       var categorydto=_mapper.Map<CategoryDto>(category);  
      return  categorydto;

    }

    public Task<CategoryForCreationDto> CreateCategoryDtoAsync(CategoryForCreationDto categoryForCreation, bool trackChanges)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<CategoryDto>> GetCategoriesAsync(bool trackChanges)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ProductDto?>> GetProductsDtosAsync(Guid CategoryId, bool trackChanges)
    {
        throw new NotImplementedException();
    }

    public void UpdateCategory(CategoryForUpdateDto categoryForUpdateDto)
    {
        throw new NotImplementedException();
    }
}