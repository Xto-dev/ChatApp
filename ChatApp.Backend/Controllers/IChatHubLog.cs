using ChatApp.Backend.Entities;

namespace ChatApp.Backend.Controllers;

public interface IChatHubLog
{
    void SendMessageError(string error);
}