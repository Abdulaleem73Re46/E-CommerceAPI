


using System.ComponentModel.DataAnnotations;
using Core.Enum.TransactionType;

namespace Core.Entities;

public class PaymentTransaction
{
    [Key]
 public    Guid Id{get;set;}

public Guid PaymentId{get;set;}
public TransactionType type{get;set;}

public decimal Amount{get;set;}
public string ExternalTransactionId {get;set;}=string.Empty;


public DateTime CreatedAt{get;set;}
public virtual Payment Payment { get; set; } = null!;


}