using Apps.TelegramBot.Events.Services;
using Apps.TelegramBot.Invocables;
using Apps.TelegramBot.Models.Dtos;
using Apps.TelegramBot.RestSharp;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Webhooks;
using RestSharp;

namespace Apps.TelegramBot.Events.Handlers.Base;

public abstract class BridgeEventHandler(InvocationContext invocationContext) : AppInvocable(invocationContext), IWebhookEventHandler
{
    private const string HardcodedId = "hardcoded_id";
    private string BridgeServiceUrl => $"{BridgeServiceBaseUrl}/webhooks/telegram";
    private string BridgeServiceBaseUrl => invocationContext.UriInfo.BridgeServiceUrl.ToString().TrimEnd('/');
    protected abstract string Event { get; }
    
    public async Task SubscribeAsync(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProvider, Dictionary<string, string> values)
    {
        var bridgeSubscribed = await IsBridgeSubscriptionExistsAsync();
        if (!bridgeSubscribed)
        {
            var request = new ApiRequest($"/setWebhook", Method.Post, Credentials)
                .AddHeader("Content-Type", "application/x-www-form-urlencoded")
                .AddParameter("url", BridgeServiceUrl);
        
            var response = await Client.ExecuteWithErrorHandling<ResultWrapper<bool>>(request);
            if (!response.Result)
            {
                throw new Exception($"Failed to subscribe to event {Event} for listener {HardcodedId}");
            }
        }
        
        var bridgeService = new BridgeService(BridgeServiceBaseUrl);
        await bridgeService.SubscribeAsync(HardcodedId, Event, values["payloadUrl"]);
    }

    public async Task UnsubscribeAsync(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProvider, Dictionary<string, string> values)
    {
        var bridgeService = new BridgeService(BridgeServiceBaseUrl);
        await bridgeService.UnsubscribeAsync(HardcodedId, Event, values["payloadUrl"]);
    }
    
    private async Task<bool> IsBridgeSubscriptionExistsAsync()
    {
        var request = new ApiRequest($"/getWebhookInfo", Method.Get, Credentials);
        var response = await Client.ExecuteWithErrorHandling<ResultWrapper<WebhookInfoDto>>(request);
        return response.Result != null! && response.Result.Url.Contains("bridge");
    }
}