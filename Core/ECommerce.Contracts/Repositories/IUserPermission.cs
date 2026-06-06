
using Core.Entities;

namespace Core.Contracts;

public interface IUserPermissionRepository
{
    Task<IEnumerable<string>> GetUserPermissionsAsync(string userId);
    Task AddPermissionAsync(UserPermission userPermission);
    Task RemovePermissionAsync(string userId, string permissionName);
    Task<bool> HasPermissionAsync(string userId, string permissionName);
}


