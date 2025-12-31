using MyTemplateClean.Application.Todos.Commands.CreateTodo;

namespace MyTemplateClean.Application.Todos.Commands.DeleteTodo;

public class DeleteTodoCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<DeleteTodoCommand, DeleteTodoResult>
{
    public async ValueTask<DeleteTodoResult> Handle(DeleteTodoCommand command, CancellationToken cancellationToken)
    {
        var todoId = TodoId.Of(command.TodoId);
        var todo = await dbContext.Todos.FindAsync([todoId], cancellationToken: cancellationToken);
        if (todo is null)
        {
            throw new TodoNotFoundException(command.TodoId);
        }

        todo.Delete();
        
        await dbContext.SaveChangesAsync(cancellationToken);
        
        return new DeleteTodoResult(true);
    }
}