

using System.Runtime.CompilerServices;
using AutoMapper;
using Core.Contracts;
using Core.Shared.DataTransferObjects;
using Service.Contracts;

namespace Service ;


public sealed class ProductService : IProductService
{

    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;

    public ProductService(IRepositoryManager repository,IMapper mapper)
    {
        
        _repository=repository;
        _mapper=mapper;
    }

    public void DeleteProduct(Guid Id)
    {
        throw new NotImplementedException();
    }

    public void DeleteProductsByCategory(Guid categoryId)
    {
        throw new NotImplementedException();
    }

    public Task<ProductDto?> GetProductByIdAsync(Guid productId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ProductDto?>> GetProductsByAsync(bool trackChanges)
    {
        throw new NotImplementedException();
    }

    public Task<int> UpdateQuantityAsync(ProductDto productDto)
    {
        throw new NotImplementedException();
    }
}