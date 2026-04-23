namespace ChatApp.Backend.Infrastructure.DTO;

public record ResponseMessageDto
{
    public Guid Id { get; init; }
    public string Text { get; init; }
    public string SentimentLabel { get; init; }
    public double SentimentScore { get; init; }
    public DateTime CreatedAt { get; init; }
}