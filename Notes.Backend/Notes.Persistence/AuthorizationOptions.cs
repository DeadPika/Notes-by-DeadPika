namespace Notes.Persistence
{
    public class AuthorizationOptions
    {
        public List<RolePermissionConfig> RolePermissions { get; set; } = [];

        public class RolePermissionConfig
        {
            public string Role { get; set; } = string.Empty;
            public List<string> Permissions { get; set; } = [];
        }
    }
}