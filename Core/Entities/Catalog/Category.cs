using System.ComponentModel.DataAnnotations;

namespace Core.Entities;




public class Category{
  [Key]
    public Guid CategoryId { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;  // ← initialized with default
    
    public DateTime CreatedAt { get; set; }
    
    public ICollection<Product> Products { get; set; } = new List<Product>();
  


}