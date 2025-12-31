namespace MyTemplateClean.Api.Apis;

public class TodoServices(IMediator mediator, ILogger<TodoServices> logger)
{
    public IMediator Mediator { get; set; } = mediator;
    public ILogger<TodoServices> Logger { get; } = logger;

}