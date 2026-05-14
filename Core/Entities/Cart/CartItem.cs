using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;


public class CartItem
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        public Guid CartId { get; set; }
        
        [Required]
        public Guid ProductId { get; set; }
        
        [Required]
        public int Quantity { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }
        
        public DateTime AddedAt { get; set; }
        
        // Navigation properties
        [ForeignKey(nameof(CartId))]
        public virtual Cart? Cart { get; set; }
        
        [ForeignKey(nameof(ProductId))]
        public virtual Product? Product { get; set; }
    }