namespace MyTemplateClean.BuildingBlocks.Events.MassTransit;

public record IntegrationEvent
{
    public Guid Id => Guid.NewGuid();
    public DateTime OccuredOn => DateTime.Now;
    public string EventType => GetType().Name;
};

