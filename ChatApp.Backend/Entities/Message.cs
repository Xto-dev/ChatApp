namespace ChatApp.Backend.Entities;

public class Message
{
    public Guid Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public SentimentLabel Label { get; set; } = SentimentLabel.Neutral;
    public double Score { get; set; } = 0.0;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}