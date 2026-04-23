using ChatApp.Backend.Entities;

namespace ChatApp.Backend.Usecases;

public interface IMessageRepository
{
    Task<List<Message>> GetRecentAsync(int count);
    Task SaveAsync(Message message);
}