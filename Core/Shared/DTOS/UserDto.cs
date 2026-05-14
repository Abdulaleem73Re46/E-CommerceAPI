namespace Core.Shared.DataTransferObjects;




public record UserDto
{
    public Guid UserId { get; init; }
    public string FullName { get; init; }
    public string Email { get; init; }
    public string Address { get; init; }
}

public record UserForCreationDto
{
    public string FullName { get; init; }
    public string Email { get; init; }
    public string Address { get; init; }
    public string Password { get; init; }
}

 public record UserForUpdateDto
{
    public string FullName { get; init; }
    public string Email { get; init; }
    public string Address { get; init; }
    public string Password { get; init; }
}       
