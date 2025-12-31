namespace MyTemplateClean.Application.Todos.Commands.DeleteTodo;

public record DeleteTodoCommand(Guid TodoId) : ICommand<DeleteTodoResult>;

public record DeleteTodoResult(bool IsSuccess);

public class DeleteOrderCommandValidator : AbstractValidator<DeleteTodoCommand>
{
    public DeleteOrderCommandValidator()
    {
        RuleFor(p => p.TodoId).NotEmpty().WithMessage("TodoId is required");
    }
}