using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Enum.PaymentMethod;
using Core.Enum.PaymentStatus;

namespace Core.Entities;



public class Payment{
[Key]
    public Guid PaymentId{get;set;}
[Required]
    public Guid OrderId{get;set;}
    [Required]
    public PaymentMethod PayMethod{get;set;}

    public DateTime PayDate{get;set;}
[Required]
    public PaymentStatus  PayStatus{get;set;}


[ForeignKey("OrderId")]
    public Order Order{get;set;}




}

