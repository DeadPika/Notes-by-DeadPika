using Notes.Persistence;
using Notes.Application;
using Notes.Application.Common.Mapping;
using Notes.Application.Interfaces;
using Notes.Infrastructure;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Notes.WebApi.Middleware;
using Notes.WebApi.Extensions;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.CookiePolicy;
using Swashbuckle.AspNetCore.SwaggerUI;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

// Настройка сервисов (аналог ConfigureServices из Startup)
builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(INotesDbContext).Assembly));
});

builder.Services.AddApplication();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddPresentation();
builder.Services.AddControllers();

builder.Services.AddApiAuthentication(builder.Configuration);

var cs = builder.Configuration.GetSection("DbConnection");

builder.Services.AddDbContext<NotesDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DbConnection")));

builder.Services.AddControllersWithViews();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
    });
});

builder.Services.AddEndpointsApiExplorer();

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

// Настройка пайплайна (аналог Configure из Startup)
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "YourSolution API V1");
        c.RoutePrefix = "swagger";
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
app.UseCors("AllowAll");

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always
});

app.UseAuthentication();    
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.Run();