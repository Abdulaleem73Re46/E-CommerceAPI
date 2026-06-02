using Core.Entities;

namespace Core.Contracts;
public interface IIdempotencyRecordRepository : IRepositoryBase<IdempotencyRecord>
{
Task<IdempotencyRecord?> GetByKeyAsync(string key);
Task<bool> KeyExistsAsync(string key);
void  Add(IdempotencyRecord idempotency);
}
