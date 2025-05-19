using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notes.Domain.Enums;
using Notes.Persistence.Entities;

namespace Notes.Persistence.Configuration
{
    public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermissionEntity>
    {
        private readonly AuthorizationOptions _authorization;

        public RolePermissionConfiguration(AuthorizationOptions authorization) => _authorization = authorization;

        public void Configure(EntityTypeBuilder<RolePermissionEntity> builder)
        {
            builder.HasKey(r => new { r.RoleId, r.PermissionId });

            builder.HasData(ParseRolePermissions().DistinctBy(rp => new { rp.RoleId, rp.PermissionId }).ToArray());
        }

        private RolePermissionEntity[] ParseRolePermissions()
        {
            if (_authorization == null || _authorization.RolePermissions == null)
            {
                Console.WriteLine("AuthorizationOptions or RolePermissions is null.");
                return Array.Empty<RolePermissionEntity>();
            }

            var permissions = _authorization.RolePermissions
                .SelectMany(rp => rp.Permissions
                    .Select(p => new RolePermissionEntity
                    {
                        RoleId = (int)Enum.Parse<Role>(rp.Role),
                        PermissionId = (int)Enum.Parse<Permission>(p)
                    }))
                .ToArray();

            Console.WriteLine("Generated RolePermissionEntities:");
            foreach (var rp in permissions)
            {
                Console.WriteLine($"RoleId: {rp.RoleId}, PermissionId: {rp.PermissionId}");
            }

            return permissions;
        }
    }

    public static class EnumerableExtensions
    {
        public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keySelector)
        {
            var seenKeys = new HashSet<TKey>();
            return source.Where(item => seenKeys.Add(keySelector(item)));
        }
    }
}