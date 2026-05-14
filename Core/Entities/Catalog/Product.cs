using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;


public class Product{
   [Key]
    public Guid ProductId { get; set; }
    
    [Required]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
    
    [Required]
    public int StockQuantity { get; set; }
    
    [MaxLength(700)]
    public string Description { get; set; } = string.Empty;
    
    [ForeignKey("Category")]
    public Guid CategoryId { get; set; }
    
    public Category Category { get; set; } = null!;  // ← null! for navigation
    
    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();



}