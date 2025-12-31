namespace MyTemplateClean.Application.Todos.Queries.GetTodosByStatus;

public class GetTodosByStatusQueryHandler(IApplicationDbContext dbContext) : IQueryHandler<GetTodosByStatusQuery, GetTodosByStatusResult>
{
    public async ValueTask<GetTodosByStatusResult> Handle(GetTodosByStatusQuery query,
        CancellationToken cancellationToken)
    {
        var todos = await dbContext
            .Todos
            .Where(k => k.Status == query.Status)
            .ToListAsync(cancellationToken);
        
        return new GetTodosByStatusResult(todos.ToTodoDtoList());
    }
}