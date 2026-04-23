using AutoMapper;
using ChatApp.Backend.Entities;
using ChatApp.Backend.Infrastructure.DTO;

namespace ChatApp.Backend.Usecases;

public class CreateMessageHandler(
    IMessageRepository _messageRepository,
    IMessageLog log,
    IMapper _mapper
    )
{    
    public async Task<ResponseMessageDto> HandleAsync(CreateMessageDto createMessageDto)
    {
        var message = new Message
        {
            Id = Guid.NewGuid(),
            Text = createMessageDto.Text,
            CreatedAt = DateTime.UtcNow
        };
    
        await _messageRepository.SaveAsync(message);

        log.MessageCreated(message);

        return _mapper.Map<ResponseMessageDto>(message);
    }
}