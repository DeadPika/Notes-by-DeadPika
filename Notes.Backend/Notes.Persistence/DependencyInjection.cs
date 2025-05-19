using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Notes.Application.Interfaces;
using Notes.Persistence.Repositories;
using Notes.Persistence.Interfaces;

namespace Notes.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DbConnection");
            services.AddDbContext<NotesDbContext>(options =>
            {
                options.UseSqlite(connectionString);
            });

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(NotesDbContext).Assembly));
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<INotesDbContext>(provider => provider.GetService<NotesDbContext>()!);
            services.AddAutoMapper(typeof(DependencyInjection).Assembly);
            services.Configure<AuthorizationOptions>(configuration.GetSection(nameof(AuthorizationOptions)));

            return services;
        }
    }
}
