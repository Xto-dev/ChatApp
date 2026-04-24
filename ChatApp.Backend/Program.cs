using ChatApp.Backend.Controllers;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();
app.UseCors();
app.MapHub<ChatHub>("/chatHub");
app.MapControllers();
app.ApplyMigrations();
app.Run();

public partial class Program { }