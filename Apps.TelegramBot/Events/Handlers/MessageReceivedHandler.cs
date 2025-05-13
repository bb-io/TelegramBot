using System;
using Apps.TelegramBot.Events.Handlers.Base;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.TelegramBot.Events.Handlers;

public class MessageReceivedHandler(InvocationContext invocationContext) : BridgeEventHandler(invocationContext)
{
    protected override string Event => "message_received";
}