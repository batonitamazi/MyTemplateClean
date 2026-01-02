namespace MyTemplateClean.BuildingBlocks.Events.MassTransit;

public record TodoStatusChangeddEvent(
    Guid TodoId,
    string TodoTitle,
    string TodoStatus
) : IntegrationEvent;