

namespace Core.Shared.DataTransferObjects;


public record CategoryDto
{
    public Guid CategoryId{get;init;}

    public string Name{get;init;}
    public DateTime CreatedAt{get;set;}

   //public ICollection<Product> 



}


public record CategoryForCreationDto
{
    

    public string Name{get;init;}=string.Empty;
    

   //public ICollection<Product> 



}
public record CategoryForUpdateDto
{
    

    public string Name{get;init;}
  

   //public ICollection<Product> 



}