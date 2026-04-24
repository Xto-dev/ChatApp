using ChatApp.Backend.Controllers;

namespace ChatApp.Backend.Infrastructure;

public class ChatHubLog(
    ILogger<MessageLog> logger
) : IChatHubLog
{
    public void SendMessageError(string error)
    {
        logger.LogError($"Error sending message: {error}");
    }
}
