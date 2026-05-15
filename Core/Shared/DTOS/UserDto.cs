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

    public required  string Password { get; init; }
}

 public record UserForUpdateDto
{
    public required string FullName { get; init; }
[EmailAddress(ErrorMessage="Invalid Email")] 

    public required string Email { get; init; }
    public required string Address { get; init; }
[MaxLength(
    public required string Password { get; init; }
}       

public record UserLoginDto{


public required string UserName{get;init;} 
public required string Password{get;init;}
} 