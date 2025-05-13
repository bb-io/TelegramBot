namespace Apps.TelegramBot.Models.Dtos;

public class PaginatedListDto<T>
{
    public IReadOnlyList<T> Items { get; init; } = Array.Empty<T>();

    public int CurrentPage { get; init; }

    public int TotalPages { get; init; }

    public int PageSize { get; init; }

    public int TotalCount { get; init; }

    public bool HasPrevious { get; init; }

    public bool HasNext { get; init; }
}
