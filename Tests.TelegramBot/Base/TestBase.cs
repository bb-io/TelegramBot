using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;
using Microsoft.Extensions.Configuration;

namespace Tests.TelegramBot.Base;

public class TestBase
{
    public IEnumerable<AuthenticationCredentialsProvider> Credentials { get; set; }

    public InvocationContext InvocationContext { get; set; }

    public FileManager FileManager { get; set; }

    public TestBase()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        Credentials = config.GetSection("ConnectionDefinition")
            .GetChildren()
            .Select(x => new AuthenticationCredentialsProvider(x.Key, x.Value!)).ToList();

        var folderLocation = config.GetSection("TestFolder").Value
            ?? throw new Exception("Test folder not found in appsettings.json");

        InvocationContext = new InvocationContext
        {
            AuthenticationCredentialsProviders = Credentials,
        };

        FileManager = new FileManager(folderLocation);
    }
}
