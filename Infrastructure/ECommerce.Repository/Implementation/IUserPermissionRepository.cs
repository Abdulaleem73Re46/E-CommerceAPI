using Core.Contracts;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class UserPermissionRepository : RepositoryBase<UserPermission>, IUserPermissionRepository
{
    public UserPermissionRepository(RepositoryContext context) : base(context)
    {
    }

    public async Task<IEnumerable<string>> GetUserPermissionsAsync(string userId)
    {
        return await FindByCondition(up => up.UserId == userId)
            .Select(up => up.PermissionName)
            .ToListAsync();
    }

    public async Task AddPermissionAsync(UserPermission userPermission)
    {
        await CreateAsync(userPermission);
        await SaveAsync();
    }

    public async Task RemovePermissionAsync(string userId, string permissionName)
    {
        var permission = await FindByCondition(up => up.UserId == userId && up.PermissionName == permissionName)
            .FirstOrDefaultAsync();
        if (permission != null)
        {
            Delete(permission);
            await SaveAsync();
        }
    }

    public async Task<bool> HasPermissionAsync(string userId, string permissionName)
    {
        return await FindByCondition(up => up.UserId == userId && up.PermissionName == permissionName)
            .AnyAsync();
    }
}