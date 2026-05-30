namespace Core.Entities;


public class IdempotencyRecord
{
    public Guid Id { get; set; }
    public string Key { get; set; } = string.Empty;
    public string ResponseJson { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}