
namespace Core.Shared.Externals;


public class PaymentResult
{
    public bool Succeeded{get;set;}
public string Message{get;set;}
public string TransactionId{get;set;}
public static PaymentResult Success(string transactionId)=> new PaymentResult{Succeeded=true,Message="SuccessFully",TransactionId=transactionId};

public static PaymentResult Failed(string errorMessage)=>new PaymentResult{Succeeded=false,Message=errorMessage};



}