using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notes.Application.Interfaces;
using Notes.Infrastructure.Authentication;

namespace Notes.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IJwtProvider,  JwtProvider>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();

            services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));

            services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();

            return services;
        }
    }
}
