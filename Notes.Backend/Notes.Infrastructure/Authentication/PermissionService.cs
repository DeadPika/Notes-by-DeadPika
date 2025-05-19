using Notes.Application.Interfaces;
using Notes.Domain.Enums;

namespace Notes.Infrastructure.Authentication
{
    public class PermissionService : IPermissionService
    {
        private readonly IUsersRepository _userRepository;

        public PermissionService(IUsersRepository userRepository) => _userRepository = userRepository;

        public Task<HashSet<Permission>> GetPermissionsAsync(Guid userID)
        {
            return _userRepository.GetUserPermissions(userID);
        }
    }
}
