﻿using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Notes.Application.Common.Behaviors;
using Notes.Application.Interfaces;
using Notes.Application.Services;
using System.Reflection;

namespace Notes.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() });
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddScoped<IUserService, UserService>();
            return services;
        }
    }
}
