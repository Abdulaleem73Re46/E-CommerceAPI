using Core.Entities;

namespace Core.Shared.DataTransferObjects;

public record CartDto
{
    public Guid CartId { get; init; }
    public Guid UserId { get; init; }
    public ICollection<CartItemDto?> CartItems { get; init; }
}
public record CartItemForCreation{

public Guid ProductId{get;init;} 
public int Quantity {get;init;} 






} 


public record CartForCreationDto
{
    public string  UserId { get; init; }
}

public record CartForUpdateDto
{
    public string UserId { get; init; }
}

public record CartItemDto
{
    public Guid CartItemId { get; init; }
    public Guid CartId { get; init; }
    public Guid ProductId { get; init; }
    public decimal UnitPrice { get; init; }
    public int Quantity { get; init; }

}

public record CartItemForUpdateDto
{

  public Guid Id{get;init;}
    public int Quantity { get; init; }
   

}

public record UpdateCartItemQuantityDto
{
    
   public Guid CartId{get;init;}

   public Guid ProductId{get;init;}

   public int Quantity{get;init;}




}