using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Core.Entities;




public class Cart{
   [Key]
  public Guid CartId{get;set;}

[Required]
  public string UserId{get;set;}

[ForeignKey("UserId")]
public User User{get;set;}  
public ICollection<CartItem> CartItems{get;set;}  



}