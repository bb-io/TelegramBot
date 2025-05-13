namespace Apps.TelegramBot.Models.Dtos;

public class ResultWrapper<T>
{
    public string Ok { get; set; } = string.Empty;
    public T Result { get; set; } = default!;
}
