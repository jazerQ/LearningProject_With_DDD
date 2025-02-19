using Core.Enums;

namespace Application
{
    public interface IPermissionService
    {
        Task<HashSet<Permission>> GetPermissionsAsync(Guid id);
    }
}