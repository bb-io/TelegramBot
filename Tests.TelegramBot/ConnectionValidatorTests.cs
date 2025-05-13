using Apps.TelegramBot.Connections;
using Blackbird.Applications.Sdk.Common.Authentication;
using Tests.TelegramBot.Base;

namespace Tests.TelegramBot;

[TestClass]
public sealed class ConnectionValidatorTests : TestBase
{
    [TestMethod]
    public async Task ValidateConnection_ShouldReturnValid_WhenCredentialsAreValid()
    {
        // Arrange
        var connectionValidator = new ConnectionValidator();

        // Act
        var result = await connectionValidator.ValidateConnection(Credentials, CancellationToken.None);

        // Assert
        Assert.IsTrue(result.IsValid);
        System.Console.WriteLine(result.IsValid);
        System.Console.WriteLine(result.Message);
    }

    [TestMethod]
    public async Task ValidateConnection_ShouldReturnInvalid_WhenCredentialsAreInvalid()
    {
        // Arrange
        var invalidCredentials = new List<AuthenticationCredentialsProvider>
        {
            new AuthenticationCredentialsProvider("botToken", "invalidToken")
        };
        var connectionValidator = new ConnectionValidator();

        // Act
        var result = await connectionValidator.ValidateConnection(invalidCredentials, CancellationToken.None);

        // Assert
        Assert.IsFalse(result.IsValid);
        System.Console.WriteLine(result.IsValid);
        System.Console.WriteLine(result.Message);
    }
}
