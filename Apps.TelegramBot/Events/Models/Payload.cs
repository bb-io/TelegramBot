using Apps.TelegramBot.Models.Responses;
using Newtonsoft.Json;

namespace Apps.TelegramBot.Events.Models;

public class Payload
{
    [JsonProperty("update_id")]
    public long UpdateId { get; set; }

    [JsonProperty("message")]
    public TelegramMessageResponse Message { get; set; } = new();
}
