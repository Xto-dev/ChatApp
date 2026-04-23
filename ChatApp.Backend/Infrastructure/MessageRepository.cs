using ChatApp.Backend.Entities;
using ChatApp.Backend.Usecases;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Backend.Infrastructure;

public class MessageRepository(
    DatabaseContext _context
) : IMessageRepository
{
    public async Task<List<Message>> GetRecentAsync(int count)
    {
        return await _context.Messages.OrderByDescending(m => m.CreatedAt).Take(count).ToListAsync();
    }

    public async Task SaveAsync(Message message)
    {
        _context.Messages.Add(message);
        await _context.SaveChangesAsync();
    }
}