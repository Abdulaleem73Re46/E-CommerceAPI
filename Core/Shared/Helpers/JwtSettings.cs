namespace Core.Shared.Helpers;


public class JWTSettings
{
    public string Key{get;set;} //this is will remove and use ENVIRNOMNEMT Variables instead 
  public string Issuer{get;set;}
  public string Audience{get;set;}
public DateTime ExpireInMinutes{get;set;} 



}