namespace Core.Entities.Exceptions;



public class ConcurrencyException:DataBaseException
{
    

public ConcurrencyException(string message):base(message)
{
    
}


public ConcurrencyException(string message,Exception innerEx):base(message
,innerEx)
{
    
}







}