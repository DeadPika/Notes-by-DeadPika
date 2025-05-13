using Microsoft.AspNetCore.Authorization;
using Notes.Domain.Enums;

namespace Notes.WebApi.Attributes
{
    public class PermissionAuthorizeAttribute : AuthorizeAttribute
    {
        public PermissionAuthorizeAttribute(params Permission[] permissions)
        {
            Policy = $"Permission_{string.Join("_", permissions)}";
        }
    }
}
