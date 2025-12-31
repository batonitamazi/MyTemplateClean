namespace MyTemplateClean.Application.Todos.Commands.CreateTodo;

public class CreateTodoCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<CreateTodoCommand, CreateTodoResult>
{
    public async ValueTask<CreateTodoResult> Handle(CreateTodoCommand command, CancellationToken cancellationToken)
    {
        var todo = CreateNewTodo(command.Todo);
        dbContext.Todos.Add(todo);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateTodoResult(todo.Id.Value);
    }

    private Todo CreateNewTodo(TodoDto todoDto)
    {
        var newTodo = Todo.Create(id: TodoId.Of(Guid.NewGuid()), title: TodoTitle.Of(todoDto.Title));
        return newTodo;
    }
}