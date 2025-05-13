using Newtonsoft.Json;

namespace Apps.TelegramBot.Models.Responses;

public class TelegramFileResponse
{
    [JsonProperty("file_id")]
    public string FileId { get; set; } = string.Empty;
    
    [JsonProperty("file_unique_id")]
    public string FileUniqueId { get; set; } = string.Empty;

    [JsonProperty("file_size")]
    public int FileSize { get; set; }

    [JsonProperty("file_path")]
    public string FilePath { get; set; } = string.Empty;
}
