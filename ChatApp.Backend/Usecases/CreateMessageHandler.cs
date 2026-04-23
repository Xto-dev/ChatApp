using AutoMapper;
using ChatApp.Backend.Entities;
using ChatApp.Backend.Infrastructure.DTO;

namespace ChatApp.Backend.Usecases;

public class CreateMessageHandler(
    IMessageRepository _messageRepository,
    ISentimentHandler _sentimentHandler,
    IMessageLog log,
    IMapper _mapper
    )
{    
    public async Task<ResponseMessageDto> HandleAsync(CreateMessageDto createMessageDto)
    {
        var sentimentResult = await _sentimentHandler.HandleAsync(createMessageDto.Text);
        var message = new Message
        {
            Id = Guid.NewGuid(),
            Text = createMessageDto.Text,
            Label = sentimentResult.SentimentLabel,
            Score = sentimentResult.SentimentScore,
            CreatedAt = DateTime.UtcNow
        };
    
        await _messageRepository.SaveAsync(message);

        log.MessageCreated(message);

        return _mapper.Map<ResponseMessageDto>(message);
    }
}