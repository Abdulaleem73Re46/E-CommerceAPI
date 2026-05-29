


namespace Core.Entities.Exceptions;


public abstract class NotFoundException : Exception
{
    
public NotFoundException(string Message):base(Message)
{
    
}



}


public class EntityNotFoundException:NotFoundException
{
    
public EntityNotFoundException(string EntityName,object id):base($"Entity {EntityName} with Id {id} Not Found"){}
   
}

public class ProductNotFoundException : NotFoundException
{
    

    public ProductNotFoundException(Guid  ProductId):base($"Product with Id {ProductId}  not found  ")
    {
        
    }
}






public class OrderNotFoundException : NotFoundException
{
    

    public OrderNotFoundException(Guid orderId ):base($"Product with Id {orderId}  not found  ")
    {
        
    }
}


public class CategoryNotFoundException : NotFoundException
{
    
public CategoryNotFoundException(Guid CategoryId):base($"Product with Id {CategoryId}  not found  "){}

}




public class CartNotFoundException:NotFoundException{


  public CartNotFoundException(string msg):base(msg){
    
  }




}






