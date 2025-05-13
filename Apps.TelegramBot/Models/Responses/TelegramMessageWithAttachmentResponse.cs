using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Files;

namespace Apps.TelegramBot.Models.Responses;

public class TelegramMessageWithAttachmentResponse
{
    [Display("Message ID")]
    public string MessageId { get; set; } = string.Empty;

    public string? Text { get; set; }

    [Display("Chat ID")]
    public string ChatId { get; set; } = string.Empty;

    [Display("Audio message")]
    public FileReference? AudioFile { get; set; }

    [Display("Has audio file")]
    public bool HasAudioFile { get; set; }
}
