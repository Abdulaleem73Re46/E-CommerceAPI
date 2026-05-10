using System.ComponentModel.DataAnnotations;

namespace Core.Entities;




public class Category{
[Key]  
  public  Guid CategoryId{get;set;}
[MaxLength(200)]
  public string? Name{get;set;}
  
  public DateTime CreatedAt{get;set;}


  public ICollection<Product> Products{get;set;}
  


}