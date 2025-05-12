using Microsoft.AspNetCore.Authorization;
using Notes.Domain.Enums;

namespace Notes.Infrastructure.Authentication
{
    public class PermissionRequirement(Permission[] permissions) : IAuthorizationRequirement
    {
        public Permission[] Permissions { get; set; } = permissions;
    }
}
