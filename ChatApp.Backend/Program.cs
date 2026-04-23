using ChatApp.Backend.Controllers;
using Microsoft.Azure.SignalR;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

var services = builder.Services;
services.AddInfrastructure(builder.Configuration);
services.AddAutoMapper(typeof(Program));
services.AddControllers();
services.AddCors(opt =>
    opt.AddDefaultPolicy(policy =>
        policy
            .WithOrigins(builder.Configuration["Frontend:Url"] ?? "http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()));

var app = builder.Build();
app.UseCors();
app.MapHub<ChatHub>("/chatHub");
app.MapControllers();

app.Run();

public partial class Program { }