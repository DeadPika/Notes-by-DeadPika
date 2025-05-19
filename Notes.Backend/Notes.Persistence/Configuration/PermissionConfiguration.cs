using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notes.Domain.Enums;
using Notes.Persistence.Entities;

namespace Notes.Persistence.Configuration
{
    public partial class PermissionConfiguration : IEntityTypeConfiguration<PermissionEntity>
    {
        public void Configure(EntityTypeBuilder<PermissionEntity> builder)
        {
            builder.HasKey(p => p.Id);

            var permissions = Enum.GetValues<Permission>()
                .Select(p => new PermissionEntity
                    {
                        Id = (int)p,
                        Name = p.ToString()
                    })
                    .ToList();

            Console.WriteLine("Permissions to seed:");
            foreach (var permission in permissions)
            {
                Console.WriteLine($"Id: {permission.Id}, Name: {permission.Name}");
            }

            var duplicates = permissions.GroupBy(p => p.Id)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key);
            if (duplicates.Any())
            {
                Console.WriteLine($"Duplicate PermissionEntity Ids found: {string.Join(", ", duplicates)}");
                throw new InvalidOperationException("Duplicate PermissionEntity Ids found.");
            }

            builder.HasData(permissions);
        }
    }
}
