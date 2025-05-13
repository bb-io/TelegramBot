using Newtonsoft.Json;

namespace Apps.TelegramBot.Models.Dtos;

public class HealthCheckDto
{
    [JsonProperty("status")]    
    public string Status { get; set; } = string.Empty;
}
