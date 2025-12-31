using System.Data;

namespace MyTemplateClean.Application.Todos.Commands.UpdateTodo;

public record UpdateTodoCommand(Guid TodoId, TodoStatus Status) : ICommand<UpdateTodoResult>;

public record UpdateTodoResult(bool IsSuccess);

public class UpdateTodoCommandValidator : AbstractValidator<UpdateTodoCommand>
{
    public UpdateTodoCommandValidator()
    {
        RuleFor(x => x.Status).NotEmpty().WithMessage("Status is required");
        RuleFor(x => x.TodoId).NotEmpty().WithMessage("Id is required");
    }
}
