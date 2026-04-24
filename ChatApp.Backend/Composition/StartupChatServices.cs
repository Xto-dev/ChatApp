using ChatApp.Backend.Infrastructure;
using ChatApp.Backend.Usecases;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using ChatApp.Backend.Controllers;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;
#pragma warning restore IDE0130

public static class StartupChatServices
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var azureSignalR = configuration["Azure:SignalR:ConnectionString"];
        var azureSQL = configuration.GetConnectionString("AzureSQL");
        var isAzure = !string.IsNullOrWhiteSpace(azureSignalR) && !string.IsNullOrWhiteSpace(azureSQL);
        var signalR = services.AddSignalR();

        if (isAzure)
        {
            services.AddDbContext<DatabaseContext>(opt => opt.UseSqlServer(azureSQL));
            signalR.AddAzureSignalR(azureSignalR);
            services.AddHttpClient<ISentimentHandler, AzureSentimentService>();
        }
        else
        {
            services.AddDbContext<DatabaseContext>(opt =>
                opt.UseSqlite("Data Source=chat_local.db"));
            services.AddScoped<ISentimentHandler, StubSentimentHandler>();
        }

        services.AddTransient<IMessageRepository, MessageRepository>();
        services.AddTransient<IMessageLog, MessageLog>();
        services.AddTransient<IChatHubLog, ChatHubLog>();
        services.AddValidatorsFromAssemblyContaining<CreateMessageValidator>();
        services.AddScoped<CreateMessageHandler>();
        services.AddScoped<GetRecentMessagesHandler>();
        services.AddAutoMapper(typeof(StartupChatServices).Assembly);
        services.AddControllers();
        services.AddCors(opt =>
            opt.AddDefaultPolicy(policy =>
                policy
                    .WithOrigins(configuration["Frontend:Url"] ?? "http://localhost:5173")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()));

        return services;
    }
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
        db.Database.EnsureCreated();
    }
}
