namespace MyTemplateClean.Application.Todos.Queries.GetTodoByTitle;

public class GetTodosByTitleQuery(string Title) : IQuery<GetTodosByTitleResult>;



public record GetTodosByTitleResult(IEnumerable<TodoDto> Todos);
