using Notes.Persistence;
using Notes.Application;
using Notes.Application.Common.Mapping;
using Notes.Application.Interfaces;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Notes.WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Настройка сервисов (аналог ConfigureServices из Startup)
builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(INotesDbContext).Assembly));
});

builder.Services.AddApplication();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddControllers();

var cs = builder.Configuration.GetSection("DbConnection");


builder.Services.AddDbContext<NotesDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DbConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
    });
});

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
}

//Вызвал Middleware, который обрабатывает исключения.
app.UseCustomExceptionHandler();

app.UseRouting();
app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();