using MyTemplateClean.BuildingBlocks.Pagination;

namespace MyTemplateClean.Application.Todos.Queries.GetTodos;

public record GetTodosQuery(PaginationRequest PaginationRequest) : IQuery<GetTodosResult>;

public record GetTodosResult(PaginatedResult<TodoDto> Todos);

