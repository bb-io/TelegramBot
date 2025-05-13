namespace Apps.TelegramBot.Models.Dtos;

public class WebhookSubsctiptionDto
{
    public Guid Id { get; set; }
    public string Url { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Event { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}