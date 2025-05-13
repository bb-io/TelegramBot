using Apps.TelegramBot.RestSharp;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Connections;
using RestSharp;

namespace Apps.TelegramBot.Connections;

public class ConnectionValidator : IConnectionValidator
{
    private static readonly ApiClient Client = new();
    
    public async ValueTask<ConnectionValidationResponse> ValidateConnection(IEnumerable<AuthenticationCredentialsProvider> authProviders, 
        CancellationToken cancellationToken)
    {
        var request = new ApiRequest("/getme", Method.Post, authProviders);

        try
        {
            await Client.ExecuteWithErrorHandling(request);
            return new()
            {
                IsValid = true
            };
        }
        catch (Exception ex)
        {
            return new()
            {
                IsValid = false,
                Message = ex.Message
            };
        }
    }
}