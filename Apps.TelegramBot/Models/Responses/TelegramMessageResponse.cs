using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.TelegramBot.Models.Responses;

public class TelegramMessageResponse
{
    [Display("Message ID"), JsonProperty("message_id")]
    public string MessageId { get; set; } = string.Empty;
    
    [JsonProperty("from")]
    public TelegramUser? From { get; set; }
    
    [JsonProperty("chat")]
    public TelegramChat? Chat { get; set; }
    
    [JsonProperty("date"), DefinitionIgnore]
    public long Date { get; set; }
    
    [JsonProperty("text")]
    public string? Text { get; set; }

    [JsonProperty("caption")]
    public string? Caption { get; set; }

    [JsonProperty("document")]
    public TelegramDocument? Document { get; set; }

    [JsonProperty("voice")]
    public TelegramVoice? Voice { get; set; }
}

public class TelegramUser
{
    [Display("User ID"), JsonProperty("id")]
    public string Id { get; set; } = string.Empty;
    
    [Display("Is bot"), JsonProperty("is_bot")]
    public bool IsBot { get; set; }
    
    [Display("[Bot] First name"), JsonProperty("first_name")]
    public string? FirstName { get; set; }
    
    [Display("[Bot] Username"), JsonProperty("username")]
    public string? Username { get; set; }
    
    [Display("Language code"), JsonProperty("language_code")]
    public string? LanguageCode { get; set; }
}

public class TelegramChat
{
    [Display("Chat ID"), JsonProperty("id")]
    public long Id { get; set; }
    
    [Display("[User] First name"), JsonProperty("first_name")]
    public string? FirstName { get; set; }
    
    [Display("[User] Username"), JsonProperty("username")]
    public string? Username { get; set; }
    
    [Display("Message type"), JsonProperty("type")]
    public string? Type { get; set; }
}

public class TelegramDocument
{
    [Display("File ID"), JsonProperty("file_id")]
    public string FileId { get; set; } = string.Empty;
    
    [Display("File name"), JsonProperty("file_name")]
    public string? FileName { get; set; }
    
    [Display("Mime type"), JsonProperty("mime_type")]
    public string? MimeType { get; set; }
    
    [Display("File size"), JsonProperty("file_size")]
    public long FileSize { get; set; }
}

public class TelegramVoice : TelegramDocument
{
    [Display("Duration"), JsonProperty("duration")]
    public long Duration { get; set; }
}
