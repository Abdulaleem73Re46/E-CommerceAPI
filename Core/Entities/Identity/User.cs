using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;


namespace Core.Entities;




public class User:IdentityUser
{
   [Required]
    public string Address { get; set; } = string.Empty;
    
    [MaxLength(50)]
    [Required]
    public string FullName { get; set; } = string.Empty;
    
    public ICollection<Order> Orders { get; set; } = new List<Order>();
    
    public Cart Cart { get; set; } = null!; 


}

