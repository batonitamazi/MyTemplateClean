using MassTransit;
using Microsoft.Extensions.Logging;
using MyTemplateClean.BuildingBlocks.Events.MassTransit;

namespace MyTemplateClean.Application.Todos.EventHandlers.Integration;

public class TodoCompletedEventConsumer(ISender sender, ILogger<TodoCompletedEventConsumer> logger) : IConsumer<TodoCompletedEvent>
{
    public async Task Consume(ConsumeContext<TodoCompletedEvent> context)
    {
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);
        await Task.CompletedTask;
    }
}