
using Core.Contracts;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class WebhookEventRepository : RepositoryBase<WebHookEvent>,
IWebHookEventRepository
{
public WebhookEventRepository(RepositoryContext context) : base(context) { }
public async Task<WebHookEvent?> GetByEventIdAsync(string eventId)
=> await FindByCondition(w => w.EventId == eventId, false).FirstOrDefaultAsync();
public async Task<bool> ExistsAsync(string eventId)
=> await FindByCondition(w => w.EventId == eventId, false)
.AnyAsync();

   
}