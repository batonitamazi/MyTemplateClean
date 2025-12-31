namespace MyTemplateClean.Application.Todos.Commands.CreateTodo;

public record CreateTodoCommand(TodoDto Todo) : ICommand<CreateTodoResult>;

public record CreateTodoResult(Guid Id);

public class CreateTodoCommandValidator : AbstractValidator<CreateTodoCommand>
{
    public CreateTodoCommandValidator()
    {
        RuleFor(x => x.Todo.Title).NotEmpty().WithMessage("Title is required");
    }
}