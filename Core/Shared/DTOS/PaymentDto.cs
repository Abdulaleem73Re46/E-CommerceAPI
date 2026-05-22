
using Core.Enum.PaymentMethod;
using Core.Enum.PaymentStatus;

namespace Core.Shared.DataTransferObjects;

public record PaymentDto
{
    public Guid PaymentId { get; init; }
    public Guid OrderId { get; init; }
    public decimal Aomunt{get;init;}
    public PaymentMethod PaymentMethod { get; init; }
    public PaymentStatus Status { get; init; }
    public DateTime PayDate { get; init; }
}

public record PaymentForCreationDto
{
     public required decimal Aomunt{get;init;}
    public PaymentMethod PaymentMethod { get; init; }
    public PaymentStatus Status{get;init;}
    
    public required Guid OrderId{get;init;}
}



public record ProcessPaymentDto
{
    
public Guid CartId{get;init;}
public string userId{get;set;}
public PaymentMethod paymentMethod{get;set;}
public string CardNumber{get;set;}
public string ExpiryDate{get;set;}
public string CVV{get;set;}



}



public record ProcessPaymentForCreation
{
   public PaymentMethod paymentMethod{get;set;}
   public string CardNumber{get;set;} 
   public string CVV{get;set;}
   



}