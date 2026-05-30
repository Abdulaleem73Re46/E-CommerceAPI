using Core.Entities;

namespace Core.Contracts;


public interface IWebhookEventRepository : IRepositoryBase<WebhookEvent>
{
Task<WebHookEvent?> GetByEventIdAsync(string eventId);
Task<bool> ExistsAsync(string eventId);
}