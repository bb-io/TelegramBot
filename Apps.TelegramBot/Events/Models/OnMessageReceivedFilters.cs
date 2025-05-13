using Blackbird.Applications.Sdk.Common;

namespace Apps.TelegramBot.Events.Models;

public class OnMessageReceivedFilters
{
    [Display("Chat ID")]
    public string? ChatId { get; set; }

    public string? Username { get; set; }
}
