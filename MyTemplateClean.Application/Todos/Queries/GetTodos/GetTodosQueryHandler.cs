using MyTemplateClean.BuildingBlocks.Pagination;

namespace MyTemplateClean.Application.Todos.Queries.GetTodos;

public class GetTodosQueryHandler(IApplicationDbContext dbContext) : IQueryHandler<GetTodosQuery, GetTodosResult>
{
    public async ValueTask<GetTodosResult> Handle(GetTodosQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;

        var totalCount = await dbContext.Todos.LongCountAsync(cancellationToken);
        
        var todos = await dbContext.Todos
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new GetTodosResult(new PaginatedResult<TodoDto>(pageIndex, pageSize, totalCount, todos.ToTodoDtoList()));
    }
}