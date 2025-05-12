using Notes.Domain.Enums;

namespace Notes.Application.Interfaces
{
    public interface IPermissionService
    {
        Task<HashSet<Permission>> GetPermissionsAsync(Guid userID);
    }
}
