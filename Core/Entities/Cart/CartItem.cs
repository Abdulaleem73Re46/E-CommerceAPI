using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;


public class CartItem{
[Key]

public Guid CartItemId{get;set;}
 [Required]
public Guid CartId{get;set;}
[Required]
public Guid ProductId{get;set;}
[Required]
public int  Quantity{get;set;}=1;
  
[ForeignKey("CartId")]
  public Cart Cart{get;set;}
[ForeignKey("ProductId")]
public Product Product{get;set;}

}