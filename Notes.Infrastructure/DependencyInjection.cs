using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notes.Application.Interfaces;
using Notes.Infrastructure.Authentication;

namespace Notes.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddScoped<IJwtProvider,  JwtProvider>();
            service.AddScoped<IPasswordHasher, PasswordHasher>();

            service.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));

            return service;
        }
    }
}
