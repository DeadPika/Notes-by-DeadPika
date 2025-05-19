using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Notes.Domain.Enums;
using Notes.Infrastructure.Authentication;
using System.Text;

namespace Notes.WebApi.Extensions
{
    public static class ApiExtensions
    {
        public static void AddApiAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                        .GetBytes(configuration.GetSection("JwtOptions").GetValue<string>("SecurityKey")!))
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request.Cookies["note-cookies"];

                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddAuthorization(options =>
            {
                foreach (Permission permission in Enum.GetValues(typeof(Permission)))
                {
                    options.AddPolicy($"Permission_{permission}", policy =>
                        policy.AddRequirements(new PermissionRequirement(new[] { permission })));
                }
            });

            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("AdminPolicy", policy =>
            //    {
            //        policy.RequireClaim("Admin", "true");
            //    });

            //    options.AddPolicy("UserPolicy", policy =>
            //    {
            //        policy.RequireClaim("User", "false");
            //    });
            //});
        }
    }
}
