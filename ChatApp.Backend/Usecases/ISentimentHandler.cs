using ChatApp.Backend.Infrastructure.DTO;

namespace ChatApp.Backend.Usecases;

public interface ISentimentHandler
{
    Task<SentimentResultDto> HandleAsync(string text);
}