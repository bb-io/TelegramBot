using Newtonsoft.Json;

namespace Apps.TelegramBot.Models.Dtos;

public class WebhookInfoDto
{
    [JsonProperty("url")]
    public string Url { get; set; } = string.Empty;
    
    [JsonProperty("has_custom_certificate")]
    public bool HasCustomCertificate { get; set; }
    
    [JsonProperty("pending_update_count")]
    public double PendingUpdateCount { get; set; }

    [JsonProperty("max_connections")]
    public double MaxConnections { get; set; }
}