using Apps.TelegramBot.Actions;
using Apps.TelegramBot.Events.Handlers;
using Apps.TelegramBot.Events.Models;
using Apps.TelegramBot.Models.Dtos;
using Apps.TelegramBot.Models.Responses;
using Apps.TelegramBot.Invocables;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;

namespace Apps.TelegramBot.Events;

[WebhookList]
public class WebhookList(InvocationContext invocationContext, IFileManagementClient fileManagementClient) : BaseInvocable(invocationContext)
{
    [Webhook("On message received", typeof(MessageReceivedHandler), Description = "Triggered when a message is received.")]
    public async Task<WebhookResponse<TelegramMessageWithAttachmentResponse>> OnMessageReceived(WebhookRequest request,
        [WebhookParameter] OnMessageReceivedFilters filters)
    {
        var payload = request.Body.ToString()!;
        var healthCheckDto = Newtonsoft.Json.JsonConvert.DeserializeObject<HealthCheckDto>(payload);
        if (healthCheckDto != null && !string.IsNullOrEmpty(healthCheckDto.Status))
        {
            return CreatePreflightResponse();
        }

        var payloadObject = Newtonsoft.Json.JsonConvert.DeserializeObject<Payload>(payload)!;
        var message = payloadObject.Message;
        
        if (!string.IsNullOrEmpty(filters.ChatId) && message.Chat?.Id.ToString() != filters.ChatId)
        {
            return CreatePreflightResponse();
        }

        if (!string.IsNullOrEmpty(filters.Username) && message.From?.Username != filters.Username)
        {
            return CreatePreflightResponse();
        }

        var response = new TelegramMessageWithAttachmentResponse
        {
            MessageId = message.MessageId,
            Text = message.Text ?? message.Caption ?? string.Empty,
            ChatId = message.Chat?.Id.ToString() ?? string.Empty,
            HasAudioFile = message.Voice != null,
            AudioFile = new()
        };

        if (message.Voice != null)
        {
            var chatActions = new ChatActions(InvocationContext, fileManagementClient);
            response.AudioFile = await chatActions.DownloadFileAsync(message.Voice.FileId);
        }

        return new()
        {
            ReceivedWebhookRequestType = WebhookRequestType.Default,
            Result = response,
            HttpResponseMessage = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
        };
    }
    
    private static WebhookResponse<TelegramMessageWithAttachmentResponse> CreatePreflightResponse()
    {
        return new()
        {
            ReceivedWebhookRequestType = WebhookRequestType.Preflight,
            Result = null!,
            HttpResponseMessage = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
        };
    }
}
