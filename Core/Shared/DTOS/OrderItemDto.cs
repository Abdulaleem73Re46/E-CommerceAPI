

namespace Core.Shared.DataTransferObjects;
public record OrderItemDto
{
    
    public Guid OrderItemId{get;set;}
   public Guid OrderId{get;set;}

   public Guid ProductId{get;set;}
   public int Quantity{get;set;}
//[Required,Column(TypeName ="decimal(18,2)")]
   public decimal PriceAtPurchase{get;set;}


//[ForeignKey("ProductId")]
   //public ICollection<Product> Products{get;set;}

  public OrderDto Order{get;set;}



}    




public record OrderItemForCreationDto
{
    
    public Guid ProductId{get;init;}
    public int Quantity{get;init;}

    



}