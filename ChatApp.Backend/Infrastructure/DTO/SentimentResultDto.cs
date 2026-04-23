using ChatApp.Backend.Entities;

namespace ChatApp.Backend.Infrastructure.DTO;

public record SentimentResultDto
{
    public SentimentLabel SentimentLabel { get; init; }
    public double SentimentScore { get; init; }
}