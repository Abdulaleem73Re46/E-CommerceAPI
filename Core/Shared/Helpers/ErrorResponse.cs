namespace Core.Shared.Helpers;

public class ErrorResponse
{
    public bool Success => false;
    public int StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;
    public List<string>? Errors { get; set; }
    public string TraceId { get; set; } = string.Empty;
}



