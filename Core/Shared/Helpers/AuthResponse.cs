
namespace  Core.Shared.Helpers;
public class AuthResponse
{
    public bool Succeeded{get;set;}
    public string Token{get;set;}=string.Empty;
    public string RefreshToken{get;set;}=string.Empty;
    public DateTime Expiration{get;set;}
public IEnumerable<string> Errors{get;set;}




}