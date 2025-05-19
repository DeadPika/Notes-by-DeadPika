using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;
using Notes.Persistence;

namespace Notes.Persistence
{
    public class NotesDbContextFactory : IDesignTimeDbContextFactory<NotesDbContext>
    {
        public NotesDbContext CreateDbContext(string[] args)
        {
            try
            {
                var optionsBuilder = new DbContextOptionsBuilder<NotesDbContext>();
                optionsBuilder.UseSqlite("Data Source=notes.db")
                              .EnableSensitiveDataLogging();

                Console.WriteLine("DbContext options configured successfully.");

                var authOptions = Options.Create(new AuthorizationOptions
                {
                    RolePermissions = new List<AuthorizationOptions.RolePermissionConfig>
                    {
                        new AuthorizationOptions.RolePermissionConfig { Role = "Admin", Permissions = new List<string> { "Create", "Read", "Update", "Delete" } },
                        new AuthorizationOptions.RolePermissionConfig { Role = "User", Permissions = new List<string> { "Read" } }
                    }
                });
                Console.WriteLine("AuthorizationOptions created successfully.");

                var context = new NotesDbContext(optionsBuilder.Options, authOptions);
                Console.WriteLine("NotesDbContext created successfully.");
                return context;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating DbContext: {ex.Message}");
                throw;
            }
        }
    }
}