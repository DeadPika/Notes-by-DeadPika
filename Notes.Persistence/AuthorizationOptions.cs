namespace Notes.Persistence
{
    public class AuthorizationOptions
    {
        public string DefaultPolicy { get; set; } = string.Empty;
        public string AdminRole { get; set; } = string.Empty;
        public List<RolePermissionConfig> RolePermissions { get; set; } = [];

        public class RolePermissionConfig
        {
            public string Role { get; set; } = string.Empty;
            public List<string> Permissions { get; set; } = [];
        }
    }
}