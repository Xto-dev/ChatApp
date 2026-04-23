using ChatApp.Backend.Entities;
using ChatApp.Backend.Infrastructure.DTO;
using ChatApp.Backend.Usecases;

namespace ChatApp.Backend.Infrastructure;

public class StubSentimentHandler : ISentimentHandler
{
    private static readonly string[] PositiveWords =
        ["great", "awesome", "good", "love", "excellent", "happy", "nice", "wonderful", "fantastic"];
    private static readonly string[] NegativeWords =
        ["bad", "terrible", "hate", "awful", "horrible", "sad", "angry", "worst", "disgusting"];

    public Task<SentimentResultDto> HandleAsync(string text)
    {
        var lower = text.ToLower();
        var pos = PositiveWords.Count(w => lower.Contains(w));
        var neg = NegativeWords.Count(w => lower.Contains(w));

        Console.WriteLine(pos);
        Console.WriteLine(neg);

        var result = (pos, neg) switch
        {
            var (p, n) when p > n => new SentimentResultDto{ SentimentLabel = SentimentLabel.Positive, SentimentScore = 0.8 },
            var (p, n) when n > p => new SentimentResultDto{ SentimentLabel = SentimentLabel.Negative, SentimentScore = 0.3 },
            _ => new SentimentResultDto { SentimentLabel = SentimentLabel.Neutral, SentimentScore = 0.5 }
        };

        return Task.FromResult(result);
    }
}