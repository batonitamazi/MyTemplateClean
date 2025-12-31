using MyTemplateClean.BuildingBlocks.Pagination;

namespace MyTemplateClean.Application.Todos.Queries.GetTodosByStatus;

public record GetTodosByStatusQuery(TodoStatus Status) : IQuery<GetTodosByStatusResult>;

public record GetTodosByStatusResult(IEnumerable<TodoDto> Todos);
