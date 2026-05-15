using Core.Entities;

namespace Core.Shared.DataTransferObjects;

public record ProductDto
{
  
    public Guid ProductId { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public decimal Price { get; init; }
    public int StockQuantity { get; init; }
    public Guid? CategoryId { get; init; }
    public CategoryDto? Category { get; init; }
}

public record CreateProductDto
{
    public string Name { get; init; }
    public string Description { get; init; }
    public decimal Price { get; init; }
    public int StockQuantity { get; init; }
    public Guid? CategoryId { get; init; }
}

public record UpdateProductDto
{
   public Guid ProductId{get;init;} 
    public string Name { get; init; }
    public string Description { get; init; }
    public decimal Price { get; init; }
    public int StockQuantity { get; init; }
    public Guid? CategoryId { get; init; }
}