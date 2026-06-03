using System.Security.Cryptography;

namespace Core.Entities.Exceptions;




public abstract class DataBaseException:Exception
{

public DataBaseException(string Message):base(Message)
{
    
}

public DataBaseException(string Message,Exception inn):base(Message,inn)
{
    
}




}