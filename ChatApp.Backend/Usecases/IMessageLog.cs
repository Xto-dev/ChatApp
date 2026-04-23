using ChatApp.Backend.Entities;

namespace ChatApp.Backend.Usecases;

public interface IMessageLog
{
    void MessageCreated(Message message);
}