using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Notes.Application.Interfaces;
using Notes.Persistence.Repositories;

namespace Notes.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection service, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DbConnection");
            service.AddDbContext<NotesDbContext>(options =>
            {
                options.UseSqlite(connectionString);
            });
            service.AddScoped<IUsersRepository, UsersRepository>();
            service.AddScoped<INotesDbContext>(provider => provider.GetService<NotesDbContext>()!);
            return service;
        }
    }
}
