

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Enum.OrderStatus;

namespace Core.Entities;


public class Order
{
 [Key]
    public Guid OrderId { get; set; }
    
    [Required]
    public string UserId { get; set; } = string.Empty;
    
    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalPrice { get; set; }
    
    public DateTime OrderDate { get; set; }
    
    [Required]
    public OrderStatus Status { get; set; }
    
    [ForeignKey("UserId")]
    public User User { get; set; } = null!;
    
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    
    public Payment Payment { get; set; } = null!;

}