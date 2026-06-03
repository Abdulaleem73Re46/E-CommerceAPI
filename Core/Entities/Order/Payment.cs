using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime;
using Core.Enum.PaymentMethod;
using Core.Enum.PaymentStatus;

namespace Core.Entities;

public class Payment
{
    [Key]
    public Guid PaymentId { get; set; }
    
  public Guid OrderId{get;set;}

    
    [Required]
    public PaymentMethods PayMethod { get; set; }
    
    [Required]
public decimal Amount {get;set;}


[MaxLength(150)]
public string ExternalTransactionId { get; set; } = string.Empty;  
public string IdempotencyKey { get; set; } = string.Empty;    

public DateTime CreatedAt { get; set; }
public DateTime? PaidAt { get; set; }


public string PaymentProvider{get;set;}="PayPal";
   
   
    
    [Required]
    public PaymentStatus PayStatus { get; set; }
    

  [ForeignKey(nameof(PaymentId))]
    public Order Order { get; set; } = null!;
    public virtual ICollection<PaymentTransaction> Transactions { get; set; } = new
List<PaymentTransaction>();
}




// public class Payment
// {
// public Guid Id { get; set; }
// public Guid OrderId { get; set; }
// public decimal Amount { get; set; }
// public PaymentStatus Status { get; set; }
// public string PaymentMethod { get; set; } = string.Empty;
// public string ExternalPaymentId { get; set; } = string.Empty;   // Stripe PaymentIntent ID
// public string IdempotencyKey { get; set; } = string.Empty;    

// public DateTime CreatedAt { get; set; }
// public DateTime? PaidAt { get; set; }
// // Navigation properties
// public virtual Order Order { get; set; } = null!;
// public virtual ICollection<PaymentTransaction> Transactions { get; set; } = new
// List<PaymentTransaction>();
// }