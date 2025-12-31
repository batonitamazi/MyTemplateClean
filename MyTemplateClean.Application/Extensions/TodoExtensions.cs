namespace MyTemplateClean.Application.Extensions;

public static class TodoExtensions
{
    public static IEnumerable<TodoDto> ToTodoDtoList(this IEnumerable<Todo> todos)
    {
        return todos.Select(ToTodoDto);
    }
        
    public static TodoDto ToTodoDto(this Todo todo)
    {
        return DtoFromTodo(todo);
    }
    private static TodoDto DtoFromTodo(Todo todo)
    {
        return new TodoDto(
            Id: todo.Id.Value,
            Title: todo.Title.Value,
            Status: todo.Status,
            CreatedAtUtc: todo.PublishDate,
            CompletedAtUtc: todo.CompleteDate
            );
    }
}