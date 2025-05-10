//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Design;
//using Microsoft.Extensions.Options;

//namespace Notes.Persistence
//{
//    public class NotesDbContextFactory : IDesignTimeDbContextFactory<NotesDbContext>
//    {
//        public NotesDbContext CreateDbContext(string[] args)
//        {
//            var optionsBuilder = new DbContextOptionsBuilder<NotesDbContext>();
//            optionsBuilder.UseSqlite("Data Source=notes.db"); // Используем строку подключения, соответствующую вашему AddDbContext

//            // Мокаем AuthorizationOptions для design-time
//            var authOptions = Options.Create(new AuthorizationOptions
//            {
//                DefaultPolicy = "DefaultPolicy",
//                AdminRole = "Admin",
//                RolePermissions = new List<AuthorizationOptions.RolePermissionConfiguration>
//                {
//                    new AuthorizationOptions.RolePermissionConfig { Role = "Admin", Permissions = new List<string> { "Read", "Write" } },
//                    new AuthorizationOptions.RolePermissionConfig { Role = "User", Permissions = new List<string> { "Read" } }
//                }
//            });

//            return new NotesDbContext(optionsBuilder.Options, authOptions);
//        }
//    }
//}