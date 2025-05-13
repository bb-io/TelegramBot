using Apps.TelegramBot.Actions;
using Apps.TelegramBot.Models.Requests;
using Blackbird.Applications.Sdk.Common.Files;
using Tests.TelegramBot.Base;

namespace Tests.TelegramBot;

[TestClass]
public sealed class ChatActionsTests : TestBase
{
    [TestMethod]
    public async Task SendMessage_ShouldSendTextMessage_WhenMessageIsProvided()
    {
        // Arrange
        var chatActions = new ChatActions(InvocationContext, FileManager);
        var request = new SendMessageRequest
        {
            ChatId = "1972139316",
            Message = "Test message from automated test"
        };

        // Act
        var response = await chatActions.SendMessage(request);

        // Assert
        Assert.IsNotNull(response);
        Assert.IsNotNull(response.MessageId);
        Assert.AreEqual(request.Message, response.Text);
        Console.WriteLine($"Message sent successfully with ID: {response.MessageId}");
    }

    [TestMethod]
    public async Task SendMessage_ShouldSendFileWithCaption_WhenFileIsProvided()
    {
        // Arrange
        var chatActions = new ChatActions(InvocationContext, FileManager);
        var fileReference = new FileReference
        {
            Name = "test.txt",
            ContentType = "text/plain"
        };
        
        var request = new SendMessageRequest
        {
            ChatId = "1972139316",
            Message = "Test file caption from automated test",
            File = fileReference
        };

        // Act
        var response = await chatActions.SendMessage(request);

        // Assert
        Assert.IsNotNull(response);
        Assert.IsNotNull(response.MessageId);
        Assert.IsNotNull(response.Document);
        Console.WriteLine($"Message with file sent successfully with ID: {response.MessageId}");
    }

    [TestMethod]
    public async Task DownloadFileAsync_ShouldDownloadVoiceMessage_WhenFileIdIsProvided()
    {
        // Arrange
        var chatActions = new ChatActions(InvocationContext, FileManager);
        var fileId = "AwACAgIAAxkBAAMiZ967_qGVX2dEs7OCIqzlqXFb5bEAAqpvAAJJgvFKEbMGPxMSp842BA";
        
        // Act
        var downloadedFile = await chatActions.DownloadFileAsync(fileId);

        // Assert
        Assert.IsNotNull(downloadedFile);
        Assert.IsNotNull(downloadedFile.Name);
        Assert.IsTrue(downloadedFile.Name.EndsWith(".oga") || downloadedFile.Name.EndsWith(".ogg"), 
            "Downloaded file should be an Ogg audio file");
        Console.WriteLine($"Voice message downloaded successfully: {downloadedFile.Name}");
    }
}
