using Notes.Persistence;
using Notes.Application;
using Notes.Application.Common.Mapping;
using Notes.Infrastructure;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Notes.WebApi.Middleware;
using Notes.WebApi.Extensions;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.OpenApi.Models;
using Notes.Persistence.Interfaces;
using Asp.Versioning;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Notes.WebApi;
using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Настройка сервисов (аналог ConfigureServices из Startup)
builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(INotesDbContext).Assembly));
});

builder.Services.AddApplication()
    .AddPersistence(builder.Configuration)
    .AddInfrastructure(builder.Configuration);

builder.Services.Configure<AuthorizationOptions>(builder.Configuration.GetSection(nameof(AuthorizationOptions)));

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(e => e.Value?.Errors.Count > 0)
            .ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value?.Errors.Select(e => e.ErrorMessage).ToArray()
            );
        return new BadRequestObjectResult(new { errors });
    };
});

builder.Services.AddControllers();

builder.Services.AddApiAuthentication(builder.Configuration);

//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//builder.Services.AddDbContext<NotesDbContext>(options =>
//    options.UseNpgsql(connectionString));

builder.Services.AddControllersWithViews();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", builder =>
    {
        builder.WithOrigins("http://localhost:5173")
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials();
    });
});

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAll", policy =>
//    {
//        policy.AllowAnyHeader();
//        policy.AllowAnyMethod();
//        policy.AllowAnyOrigin();
//    });
//});

builder.Services.AddEndpointsApiExplorer();

//builder.Services.AddSwaggerGen(config =>
//{
//    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
//    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
//    config.IncludeXmlComments(xmlPath);
//    config.SwaggerDoc("v1", new OpenApiInfo
//    {
//        Title = "Notes.WebApi",
//        Version = "v1"
//    });
//});


// Регистрируем Asp.Versioning.Mvc
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Инициализация базы данных (аналог кода из Main)
using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    try
    {
        var context = serviceProvider.GetRequiredService<NotesDbContext>();
        DbInitializer.Initialize(context);
    }
    catch (Exception exception)
    {
        // Здесь можно добавить логирование ошибки, например:
        var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(exception, "An error occurred while initializing the database.");
    }
}

var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

// Настройка пайплайна (аналог Configure из Startup) и Swagger.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger(); 
    app.UseSwaggerUI(config =>
    {
        foreach (var description in provider.ApiVersionDescriptions)
        {
            config.SwaggerEndpoint(
                $"/swagger/{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant());
            config.RoutePrefix = string.Empty;
        }
    });
}

//Вызвал Middleware, который обрабатывает исключения.
app.UseCustomExceptionHandler();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(app.Environment.ContentRootPath, "Styles")),
    RequestPath = "/styles"
});

app.UseRouting();
app.UseHttpsRedirection();
app.UseCors("AllowFrontend");
//app.UseCors("AllowAll");

//app.UseCookiePolicy(new CookiePolicyOptions
//{
//    MinimumSameSitePolicy = SameSiteMode.Strict,
//    HttpOnly = HttpOnlyPolicy.Always,
//    Secure = CookieSecurePolicy.Always
//});

app.UseAuthentication();    
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.Run();;