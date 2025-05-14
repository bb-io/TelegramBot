using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.TelegramBot.DataHandlers.Static;

public class ParseModeDataHandler : IStaticDataSourceItemHandler
{
    public IEnumerable<DataSourceItem> GetData()
    {
        return
        [
            new("HTML", "HTML"),
            new("Markdown", "Markdown")
        ];
    }
}
