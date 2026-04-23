using AutoMapper;
using ChatApp.Backend.Infrastructure.DTO;

namespace ChatApp.Backend.Usecases;

public class GetRecentMessagesHandler(
    IMessageRepository _messageRepository,
    IMapper _mapper,
    IMessageLog log
)
{
    public async Task<List<ResponseMessageDto>> HandleAsync(int count)
    {
        var messages = await _messageRepository.GetRecentAsync(count);
        return _mapper.Map<List<ResponseMessageDto>>(messages);
    }
}