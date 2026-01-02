namespace MyTemplateClean.BuildingBlocks.Events.MassTransit;

public record TodoCompletedEvent(
    Guid TodoId,
    string TodoTitle,
    string TodoStatus
) : IntegrationEvent;