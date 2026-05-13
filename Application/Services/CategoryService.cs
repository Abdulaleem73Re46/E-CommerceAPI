using System.Net.Cache;
using System.Runtime.CompilerServices;
using AutoMapper;
using Core.Contracts;
using Core.Entities;
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

    public async  Task<CategoryDto> CreateCategoryDtoAsync(CategoryForCreationDto categoryForCreation, bool trackChanges)
    {  
        var categoryEntity=_mapper.Map<Category>(categoryForCreation);
        _repository.CategoryRepository.CreateCategory(categoryEntity);
       await _repository.SaveAsync();
       var categoryToReturn=_mapper.Map<CategoryDto>(categoryEntity);
       return categoryToReturn;
        
    }

    public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync(bool trackChanges)
    {
        var caties=await _repository.CategoryRepository.GetCategorysAsync(trackChanges);
      if(caties is null)throw new Exception("Not Found ");
      var catiesToReturn=_mapper.Map<IEnumerable<CategoryDto>>(caties);
      return catiesToReturn;

    }

    public async Task<IEnumerable<ProductDto?>> GetProductsDtosAsync(Guid CategoryId, bool trackChanges)
    {
        var cat=await _repository.CategoryRepository.GetCategoryAsync(CategoryId,trackChanges);
        if (cat is null)throw new Exception("NotFound");
       var pro= await _repository.CategoryRepository.GetCategoryWithProductsAsync(CategoryId);
          
        var dto=_mapper.Map<IEnumerable<ProductDto>>(pro);
        return dto;  


      

    }

    public async Task UpdateCategory(CategoryForUpdateDto categoryForUpdateDto)
    {
            // var cati=await _repository.CategoryRepository.GetCategoryAsync(categoryForUpdateDto.CategoryId,false);
            
            // _mapper.Map(categoryForUpdateDto,cati);

            // await _repository.CategoryRepository.UpdateCategory()
            throw new NotImplementedException();
    }

    void ICategoryService.UpdateCategory(CategoryForUpdateDto categoryForUpdateDto)
    {
        throw new NotImplementedException();
    }
}