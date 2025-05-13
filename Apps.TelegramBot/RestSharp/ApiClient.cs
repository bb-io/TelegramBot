using Apps.TelegramBot.Constants;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Utils.RestSharp;
using RestSharp;

namespace Apps.TelegramBot.RestSharp;

public class ApiClient() : BlackBirdRestClient(new RestClientOptions { BaseUrl = new(Urls.TelegramApi), ThrowOnAnyError = false })
{
    protected override Exception ConfigureErrorException(RestResponse response)
    {
        var errorMessage = response.Content ?? response.ErrorMessage ?? "Unknown error";
        return new PluginApplicationException(errorMessage);
    }
}