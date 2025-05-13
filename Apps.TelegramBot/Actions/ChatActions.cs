using Apps.TelegramBot.Invocables;
using Apps.TelegramBot.Models.Dtos;
using Apps.TelegramBot.Models.Requests;
using Apps.TelegramBot.Models.Responses;
using Apps.TelegramBot.RestSharp;
using Apps.TelegramBot.Models.Enums;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Files;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Utils.Extensions.Files;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using RestSharp;

namespace Apps.TelegramBot.Actions;

[ActionList]
public class ChatActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient) : AppInvocable(invocationContext)
{
    [Action("Send message", Description = "Send a message to a chat for specified user")]
    public async Task<TelegramMessageResponse> SendMessage([ActionParameter] SendMessageRequest sendMessageRequest) 
    {
        if(sendMessageRequest.File == null)
        {
            return await SendMessageAsync(sendMessageRequest);
        }
        else
        {
            return await SendFileAsync(sendMessageRequest);
        }
    }

    public async Task<FileReference> DownloadFileAsync(string fileId) 
    {
        var request = new ApiRequest($"/getFile?file_id={fileId}", Method.Get, Credentials);
        var wrapper = await Client.ExecuteWithErrorHandling<ResultWrapper<TelegramFileResponse>>(request);

        var downloadFileRequest = ApiRequest.CreateFileDownloadRequest(wrapper.Result.FilePath, Credentials);
        var fileResponse = await Client.ExecuteWithErrorHandling(downloadFileRequest);
        var memoryStream = new MemoryStream(fileResponse.RawBytes!);
        memoryStream.Position = 0;

        var fileName = wrapper.Result.FilePath.Split('/').Last();
        var mimeTypes = MimeTypes.GetMimeType(fileName);
        return await fileManagementClient.UploadAsync(memoryStream, mimeTypes, fileName);
    }

    private async Task<TelegramMessageResponse> SendMessageAsync(SendMessageRequest sendMessageRequest) 
    {
        long? replyToMessageId = string.IsNullOrEmpty(sendMessageRequest.ReplyToMessageId) ? null : long.Parse(sendMessageRequest.ReplyToMessageId);
        var request = new ApiRequest("/sendMessage", Method.Post, Credentials)
            .AddJsonBody(new
            {
                chat_id = sendMessageRequest.ChatId,
                text = sendMessageRequest.Message,
                reply_to_message_id = replyToMessageId
            });
        
        var wrapper = await Client.ExecuteWithErrorHandling<ResultWrapper<TelegramMessageResponse>>(request);
        return wrapper.Result;
    }

    private async Task<TelegramMessageResponse> SendFileAsync(SendMessageRequest sendMessageRequest) 
    {
        var fileStream = await fileManagementClient.DownloadAsync(sendMessageRequest.File!);
        var bytes = await fileStream.GetByteData();

        var request = new ApiRequest("/sendDocument", Method.Post, Credentials)
            .AddParameter("chat_id", sendMessageRequest.ChatId)
            .AddParameter("caption", sendMessageRequest.Message)
            .AddFile("document", bytes, sendMessageRequest.File!.Name, sendMessageRequest.File!.ContentType);

        if (!string.IsNullOrEmpty(sendMessageRequest.ReplyToMessageId))
        {
            request.AddParameter("reply_to_message_id", sendMessageRequest.ReplyToMessageId);
        }
        
        var wrapper = await Client.ExecuteWithErrorHandling<ResultWrapper<TelegramMessageResponse>>(request);
        return wrapper.Result;
    }
}