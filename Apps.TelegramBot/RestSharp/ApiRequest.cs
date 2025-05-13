using Apps.TelegramBot.Constants;
using Apps.TelegramBot.Models.Enums;
using Blackbird.Applications.Sdk.Common.Authentication;
using RestSharp;

namespace Apps.TelegramBot.RestSharp;

public class ApiRequest : RestRequest
{    
    public ApiRequest() : base()
    { }

    public ApiRequest(string resource, Method method, IEnumerable<AuthenticationCredentialsProvider> creds) : base(resource, method)
    {
        var botToken = creds.First(x => x.KeyName == CredsNames.BotToken);
        var encodedBotToken = botToken.Value.Replace(":", "%3A");
        Resource = $"/bot{encodedBotToken}" + resource;
    }

    public static ApiRequest CreateFileDownloadRequest(string path, IEnumerable<AuthenticationCredentialsProvider> creds)
    {
        var botToken = creds.First(x => x.KeyName == CredsNames.BotToken);
        var encodedBotToken = botToken.Value.Replace(":", "%3A");

        var request = new ApiRequest
        {
            Resource = $"/file/bot{encodedBotToken}/" + path,
            Method = Method.Get
        };

        return request;
    }
}