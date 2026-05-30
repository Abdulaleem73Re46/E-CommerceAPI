


using System.ComponentModel.DataAnnotations;
namespace Core.Entities;

public class WebHookEvent
{
    [Key]
public Guid Id{get;set;}

public string EventId { get; set; }=string.Empty;

public string EventType { get; set; }=string.Empty;

public bool Processed { get; set; }
public DateTime ReceivedAt{get;set;}








}