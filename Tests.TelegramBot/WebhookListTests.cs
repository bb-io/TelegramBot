using Apps.TelegramBot.Events;
using Apps.TelegramBot.Events.Models;
using Apps.TelegramBot.Models.Responses;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Tests.TelegramBot.Base;

namespace Tests.TelegramBot;

[TestClass]
public class WebhookListTests : TestBase
{
    private readonly WebhookList _webhookList;
    private readonly WebhookRequest _request;
    private const string ValidMessageJson = @"{
      ""update_id"": 1111,
      ""message"": {
        ""message_id"": 2,
        ""from"": {
          ""id"": 1111,
          ""is_bot"": false,
          ""first_name"": ""Golub"",
          ""username"": ""golubino"",
          ""language_code"": ""en""
        },
        ""chat"": {
          ""id"": 1111,
          ""first_name"": ""Golub"",
          ""username"": ""golubino"",
          ""type"": ""private""
        },
        ""date"": 1742046658,
        ""text"": ""Hello, world!""
      }
    }";
    
    private const string HealthCheckJson = @"{""status"": ""ok""}";
    private const string ValidChatId = "1972139316";
    private const string ValidUsername = "vitaliybezugly";

    public WebhookListTests()
    {
        _webhookList = new WebhookList(InvocationContext, FileManager);
        _request = new WebhookRequest
        {
            Headers = new Dictionary<string, string>(),
            QueryParameters = new Dictionary<string, string>(),
            Url = "https://example.com",
            HttpMethod = HttpMethod.Post
        };
    }

    [TestMethod]
    public async Task OnMessageReceived_ShouldProcessMessage_WhenValidJsonIsProvided()
    {
        // Arrange
        _request.Body = ValidMessageJson;
        var filters = new OnMessageReceivedFilters();
        
        // Act
        var response = await _webhookList.OnMessageReceived(_request, filters);
        
        // Assert
        Assert.IsNotNull(response);
        Assert.AreEqual(WebhookRequestType.Default, response.ReceivedWebhookRequestType);
        Assert.IsNotNull(response.Result);
        Assert.AreEqual("2", response.Result.MessageId);
        Assert.AreEqual("Hello, world!", response.Result.Text);
        Assert.IsNotNull(response.Result.AudioFile);
        Assert.AreEqual(System.Net.HttpStatusCode.OK, response.HttpResponseMessage!.StatusCode);
    }

    [TestMethod]
    public async Task OnMessageReceived_ShouldReturnMessage_WhenFiltersMatch()
    {
        // Arrange
        _request.Body = ValidMessageJson;
        var filters = new OnMessageReceivedFilters
        {
            ChatId = ValidChatId,
            Username = ValidUsername
        };
        
        // Act
        var response = await _webhookList.OnMessageReceived(_request, filters);
        
        // Assert
        AssertValidResponse(response);
    }

    [TestMethod]
    public async Task OnMessageReceived_ShouldReturnPreflight_WhenChatIdDoesNotMatch()
    {
        // Arrange
        _request.Body = ValidMessageJson;
        var filters = new OnMessageReceivedFilters
        {
            ChatId = "different-chat-id"
        };
        
        // Act
        var response = await _webhookList.OnMessageReceived(_request, filters);
        
        // Assert
        AssertPreflightResponse(response);
    }

    [TestMethod]
    public async Task OnMessageReceived_ShouldReturnPreflight_WhenUsernameDoesNotMatch()
    {
        // Arrange
        _request.Body = ValidMessageJson;
        var filters = new OnMessageReceivedFilters
        {
            Username = "different-username"
        };
        
        // Act
        var response = await _webhookList.OnMessageReceived(_request, filters);
        
        // Assert
        AssertPreflightResponse(response);
    }

    [TestMethod]
    public async Task OnMessageReceived_ShouldReturnPreflight_WhenHealthCheckIsProvided()
    {
        // Arrange
        _request.Body = HealthCheckJson;
        var filters = new OnMessageReceivedFilters();
        
        // Act
        var response = await _webhookList.OnMessageReceived(_request, filters);
        
        // Assert
        AssertPreflightResponse(response);
    }
    
    private void AssertValidResponse(WebhookResponse<TelegramMessageWithAttachmentResponse> response) 
    {
        Assert.IsNotNull(response);
        Assert.AreEqual(WebhookRequestType.Default, response.ReceivedWebhookRequestType);
        Assert.IsNotNull(response.Result);
        Assert.AreEqual("2", response.Result.MessageId);
        Assert.AreEqual("Hello, world!", response.Result.Text);
    }
    
    private void AssertPreflightResponse(WebhookResponse<TelegramMessageWithAttachmentResponse> response)
    {
        Assert.IsNotNull(response);
        Assert.AreEqual(WebhookRequestType.Preflight, response.ReceivedWebhookRequestType);
        Assert.IsNull(response.Result);
        Assert.AreEqual(System.Net.HttpStatusCode.OK, response.HttpResponseMessage!.StatusCode);
    }
}
