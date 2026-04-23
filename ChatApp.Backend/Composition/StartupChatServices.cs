using ChatApp.Backend.Infrastructure;
using ChatApp.Backend.Usecases;
using Microsoft.EntityFrameworkCore;
using FluentValidation;

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
        

        services.AddCors(opt =>
            opt.AddDefaultPolicy(policy =>
                policy
                    .WithOrigins("http://localhost:5173", "https://chat-frontend-app.azurewebsites.net")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()));

        if (isAzure)
        {
            services.AddDbContext<DatabaseContext>(opt => opt.UseSqlServer(azureSQL));
            signalR.AddAzureSignalR(azureSignalR);
            //services.AddHttpClient<ISentimentService, AzureSentimentService>();
            //services.AddScoped<IChatPublisher, AzureChatPublisher>();
        }
        else
        {
            services.AddDbContext<DatabaseContext>(opt =>
                opt.UseSqlite("Data Source=chat_local.db"));
            //services.AddScoped<ISentimentService, StubSentimentService>();
            //services.AddScoped<IChatPublisher, LocalChatPublisher>();
        }

        services.AddTransient<IMessageRepository, MessageRepository>();
        services.AddTransient<IMessageLog, MessageLog>();
        services.AddValidatorsFromAssemblyContaining<CreateMessageValidator>();
        services.AddScoped<CreateMessageHandler>();
        services.AddScoped<GetRecentMessagesHandler>();

        return services;
    }
}
