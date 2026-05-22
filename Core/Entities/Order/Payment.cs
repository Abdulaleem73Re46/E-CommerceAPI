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
    
  
    
    [Required]
    public PaymentMethod PayMethod { get; set; }
    
    [Required]
public decimal Amount {get;set;}

    public DateTime PayDate { get; set; }
    
    [Required]
    public PaymentStatus PayStatus { get; set; }
    
  
    public Order Order { get; set; } = null!;
}