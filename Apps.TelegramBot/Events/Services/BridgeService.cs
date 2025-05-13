using Apps.TelegramBot.Models.Dtos;
using RestSharp;

namespace Apps.TelegramBot.Events.Services;

public class BridgeService(string bridgeServiceUrl)
{
    private string BridgeServiceUrl { get; set; } = bridgeServiceUrl;

    public async Task SubscribeAsync(string id, string @event, string url)
    {
        var client = new RestClient(BridgeServiceUrl);
        var request = new RestRequest($"/{id}/{@event}", Method.Post)
            .AddHeader("Blackbird-Token", ApplicationConstants.BlackbirdToken)
            .AddBody(url);

        var response = await client.ExecuteAsync(request);
        if (!response.IsSuccessful)
        {
            throw new Exception($"Failed to subscribe to event {@event} for listener {id}");
        }
    }

    public async Task UnsubscribeAsync(string id, string @event, string url)
    {
        var client = new RestClient(BridgeServiceUrl);
        var subscriptionsRequest = new RestRequest($"/{id}/{@event}")
            .AddHeader("Blackbird-Token", ApplicationConstants.BlackbirdToken);
        
        var subscribersDtos = await client.GetAsync<List<BridgeSubscribersDto>>(subscriptionsRequest);
        var webhook = subscribersDtos!.FirstOrDefault(w => w.Value == url);
        if (webhook != null)
        {
            var deleteRequest = new RestRequest($"/{id}/{@event}/{webhook.Id}", Method.Delete);
            deleteRequest.AddHeader("Blackbird-Token", ApplicationConstants.BlackbirdToken);
            await client.DeleteAsync(deleteRequest);
        }
    }
    
    public async Task<bool> IsAnySubscriberExistAsync(string @event, string id)
    {
        var client = new RestClient(BridgeServiceUrl);
        var request = new RestRequest($"/{id}/{@event}")
            .AddHeader("Blackbird-Token", ApplicationConstants.BlackbirdToken);
        
        var response = await client.GetAsync<List<BridgeSubscribersDto>>(request);
        return response?.Any() ?? false;
    }
}