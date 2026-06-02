using Core.Entities;

namespace Core.Contracts;


public interface IWebHookEventRepository : IRepositoryBase<WebHookEvent>
{
Task<WebHookEvent?> GetByEventIdAsync(string eventId);
Task<bool> ExistsAsync(string eventId);
}