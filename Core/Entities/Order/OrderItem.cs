using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

[Table("OrderItems")]
public class OrderItem
{
    [Key]
    public Guid OrderItemId { get; set; }
    
    [Required]
    public Guid OrderId { get; set; }
    
    [Required]
    public Guid ProductId { get; set; }
    
    [Required]
    public int Quantity { get; set; }
    
    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal PriceAtPurchase { get; set; }
    
    [ForeignKey("ProductId")]
    public Product Product { get; set; } = null!;  // ← Changed from "Products" to "Product"
    
    [ForeignKey("OrderId")]
    public Order Order { get; set; } = null!;
}