
namespace Core.Shared.DataTransferObjects;

public record TokenDto
{
    
    public string AccessToken{get;init;}=string.Empty;
    public string RefreshToken{get;init;}=string.Empty;

}



public record RefreshTokenRequestDto
{
    
   public string AccessToken{get;init;}=string.Empty;
    public string RefreshToken{get;init;}=string.Empty;

}
