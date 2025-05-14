using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Metadata;

namespace Apps.TelegramBot;

public class Application : IApplication, ICategoryProvider
{
    public IEnumerable<ApplicationCategory> Categories 
    {
        get => [
            ApplicationCategory.SocialMedia,
            ApplicationCategory.Communication
        ];
        set { }
    }

    public T GetInstance<T>()
    {
        throw new NotImplementedException();
    }
}