using ChatApp.Backend.Entities;
using ChatApp.Backend.Infrastructure.DTO;
using ChatApp.Backend.Usecases;

namespace ChatApp.Backend.Infrastructure;

public class AzureSentimentService : ISentimentHandler
{
    private readonly HttpClient _http;
    private readonly string _endpoint;
    private readonly string _key;

    public AzureSentimentService(HttpClient http, IConfiguration config)
    {
        _http = http;
        _endpoint = config["AzureTextAnalytics:Endpoint"] ?? string.Empty;
        _key = config["AzureTextAnalytics:Key"] ?? string.Empty;
    }

    public async Task<SentimentResultDto> HandleAsync(string text)
    {
        if (string.IsNullOrEmpty(_endpoint) || string.IsNullOrEmpty(_key))
            return new SentimentResultDto { SentimentLabel = SentimentLabel.Neutral, SentimentScore = 0.5 };

        _http.DefaultRequestHeaders.Clear();
        _http.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _key);

        var body = new { documents = new[] { new { id = "1", language = "en", text } } };

        var response = await _http.PostAsJsonAsync(
            $"{_endpoint}/text/analytics/v3.1/sentiment", body);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<AzureResponse>();
        var doc = result?.Documents?.FirstOrDefault();
        if (doc == null) return new SentimentResultDto { SentimentLabel = SentimentLabel.Neutral, SentimentScore = 0.5 };

        return doc.Sentiment switch
        {
            "positive" => new SentimentResultDto { SentimentLabel = SentimentLabel.Positive, SentimentScore = doc.ConfidenceScores.Positive },
            "negative" => new SentimentResultDto { SentimentLabel = SentimentLabel.Negative, SentimentScore = doc.ConfidenceScores.Negative },
            _ => new SentimentResultDto{ SentimentLabel = SentimentLabel.Neutral, SentimentScore = doc.ConfidenceScores.Neutral }
        };
    }
}

file record AzureResponse(List<AzureDoc>? Documents);
file record AzureDoc(string Sentiment, AzureScores ConfidenceScores);
file record AzureScores(double Positive, double Neutral, double Negative);
