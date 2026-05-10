using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;


namespace Core.Entities;




public class User:IdentityUser
{
   [Required]
   public string Address{get;set;}
   [MaxLength(50)]
   [Required]
   public string FullName{get;set;}
    

   public ICollection<Order> Orders{get;set;} 
public Cart Cart{get;set;} 


}

