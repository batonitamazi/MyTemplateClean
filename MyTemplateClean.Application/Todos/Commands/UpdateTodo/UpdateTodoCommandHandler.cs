
using MassTransit;
using MyTemplateClean.BuildingBlocks.Events.MassTransit;

namespace MyTemplateClean.Application.Todos.Commands.UpdateTodo;

public class UpdateTodoCommandHandler(IApplicationDbContext dbContext, IPublishEndpoint publishEndpoint) : ICommandHandler<UpdateTodoCommand, UpdateTodoResult>
{
    public async ValueTask<UpdateTodoResult> Handle(UpdateTodoCommand command, CancellationToken cancellationToken)
    {
        var todoId = TodoId.Of(command.TodoId);
        var todo = await dbContext.Todos.FindAsync([todoId], cancellationToken: cancellationToken);
        if (todo is null)
        {
            throw new TodoNotFoundException(command.TodoId);
        }

        todo.Update(command.Status);
        
        
        await dbContext.SaveChangesAsync(cancellationToken);

        if (todo.Status == TodoStatus.Completed)
        {
            var eventMesage = new TodoCompletedEvent(todo.Id.Value, todo.Title.Value, todo.Status.ToString());
            await publishEndpoint.Publish(eventMesage, cancellationToken);
        }
        return new UpdateTodoResult(true);
    }
}