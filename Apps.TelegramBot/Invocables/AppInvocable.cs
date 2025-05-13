using Apps.TelegramBot.RestSharp;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.TelegramBot.Invocables;

public class AppInvocable : BaseInvocable
{
    protected ApiClient Client { get; }

    protected IEnumerable<AuthenticationCredentialsProvider> Credentials =>
        InvocationContext.AuthenticationCredentialsProviders;

    protected AppInvocable(InvocationContext invocationContext) : base(invocationContext)
    {
        Client = new();
    }
}