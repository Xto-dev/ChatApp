using ChatApp.Backend.Entities;
using ChatApp.Backend.Usecases;

namespace ChatApp.Backend.Infrastructure;

public class MessageLog(
    ILogger<MessageLog> logger
    ) : IMessageLog
{
    public void MessageCreated(Message message)
    {
        logger.LogInformation("Message created: {MessageId}, Text: {MessageText}, CreatedAt: {CreatedAt}", message.Id, message.Text, message.CreatedAt);
    }
}