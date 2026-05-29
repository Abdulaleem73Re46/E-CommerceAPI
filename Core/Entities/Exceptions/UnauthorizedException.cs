
namespace Core.Entities.Exceptions;

public class UnauthorizedException:Exception
{
    public UnauthorizedException(string Message="You are not authorized to access this resource."):base(Message)
    {
        
    }





}


public class InvalidCredentialsException:UnauthorizedAccessException
{
    
public InvalidCredentialsException():base("Invalid username or password! "){}
}