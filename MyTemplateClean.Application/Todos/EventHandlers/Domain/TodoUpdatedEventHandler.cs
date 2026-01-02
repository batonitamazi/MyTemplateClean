using MassTransit;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using MyTemplateClean.Domain.Events;

namespace MyTemplateClean.Application.Todos.EventHandlers.Domain;

public class TodoUpdatedEventHandler(IPublishEndpoint publishEndpoint, IFeatureManager featureManager, ILogger<TodoUpdatedEventHandler> logger): INotificationHandler<TodoUpdatedEvent>
{
    public async ValueTask Handle(TodoUpdatedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event Handled: {DomainEvent}", domainEvent.GetType().Name);

        if(await featureManager.IsEnabledAsync("TodoFullfilment"))
        {
            var todoUpdatedIntegrationEvent = domainEvent.todo.ToTodoDto();
            await publishEndpoint.Publish(todoUpdatedIntegrationEvent, cancellationToken);
        }
    }
}