using System.ComponentModel.DataAnnotations;

namespace Core.Shared.DataTransferObjects;




public record UserDto
{
    public Guid UserId { get; init; }
    public string FullName { get; init; }
    public string Email { get; init; }
    public string Address { get; init; }
}

public record UserForRegisterDto
{   
    public required string FullName { get; init; }

[EmailAddress(ErrorMessage="Invalid Email Address ") ] 
    public required string Email { get; init; }

    public required string Address { get; init; }

[DataType(DataType.Password)]
[StringLength(100,ErrorMessage="The password must be at characters long and more than 8 ",MinimumLength =8)] 
 public required  string Password { get; init; }

     public ICollection<string>? Roles{get;init;}
     
}

 public record UserForUpdateDto
{
    public required string FullName { get; init; }
[EmailAddress(ErrorMessage="Invalid Email")] 

    public required string Email { get; init; }
    public required string Address { get; init; }

    public required string Password { get; init; }
}       

public record UserLoginDto{

[Required(ErrorMessage ="User Name is required")]
public  string UserName{get;init;} 
[Required(ErrorMessage ="User Name is required")]
public  string Password{get;init;}

} 