using Core.Contracts;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
namespace Repository;
public class IdempotencyRecordRepository : RepositoryBase<IdempotencyRecord>,
IIdempotencyRecordRepository
{
public IdempotencyRecordRepository(RepositoryContext context) : base(context) { }
public async Task<IdempotencyRecord?> GetByKeyAsync(string key)
=> await FindByCondition(i => i.Key == key, false)
.FirstOrDefaultAsync();
public async Task<bool> KeyExistsAsync(string key)
=> await FindByCondition(i => i.Key == key, false)
.AnyAsync();



public void  Add(IdempotencyRecord idempotencyRecord)=>Create(idempotencyRecord);


}