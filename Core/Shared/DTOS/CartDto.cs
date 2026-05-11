namespace Core.Shared.DataTransferObjects;


public record CartDto
{
    public Guid CartId{get;init;}

    public Guid UserId{get;init;}
    //public DateTime CreatedAt{get;set;}

   //public ICollection<Product> 



}


public record CartForCreationDto
{
    

    public Guid UserId{get;init;}

    //public DateTime CreatedAt{get;set;}

   //public ICollection<Product> 

}





public record CartForUpdateDto
{
    
  public Guid UserId{get;init;}



}






public record CartItemDto
{
    
   public Guid CartItemId{get;init;}
   public Guid CartId{get;init;}
   public Guid ProductId{get;init;}
   public int Quantity{get;init;}

}

public record CartItemForUpdateDto
{
   
   public int Quantity{get;init;}
   



}