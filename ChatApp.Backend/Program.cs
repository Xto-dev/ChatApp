using ChatApp.Backend.Controllers;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

var services = builder.Services;
services.AddInfrastructure(builder.Configuration);
services.AddAutoMapper(typeof(Program));
services.AddControllers();

var app = builder.Build();
//app.UseHttpsRedirection();
app.UseCors();
app.MapHub<ChatHub>("/chatHub");
app.MapControllers();

app.Run();

public partial class Program { }