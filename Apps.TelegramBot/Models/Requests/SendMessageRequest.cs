using Apps.TelegramBot.DataHandlers.Static;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Files;

namespace Apps.TelegramBot.Models.Requests;

public class SendMessageRequest
{
    [Display("Chat ID")]
    public string ChatId { get; set; } = string.Empty;

    [Display("Message")]
    public string Message { get; set; } = string.Empty;

    [Display("Reply to message ID")]
    public string? ReplyToMessageId { get; set; }

    [Display("Parse mode"), StaticDataSource(typeof(ParseModeDataHandler))]
    public string? ParseMode { get; set; }

    public FileReference? File { get; set; }
}
