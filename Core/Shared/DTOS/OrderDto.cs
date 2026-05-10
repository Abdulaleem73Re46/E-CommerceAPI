
namespace Core.Shared.DataTransferObjects;




public record OrderDto
{
    

    public Guid OrderId{get;init;}
    public Guid UserId{get;init;}
     public decimal TotalPrice{get;init;}
  public DateTime OrderDate {get;init;}
//public 
   //public Guid {get;init;}
    
    }

public record OrderForCreationDto
{
   public decimal TotalPrice{get;init;}
   


}