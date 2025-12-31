using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MyTemplateClean.Application.Todos.Commands.CreateTodo;
using MyTemplateClean.Application.Todos.Commands.DeleteTodo;
using MyTemplateClean.Application.Todos.Queries.GetTodoByTitle;
using MyTemplateClean.Application.Todos.Queries.GetTodos;
using MyTemplateClean.Application.Todos.Queries.GetTodosByStatus;
using MyTemplateClean.Domain.Enums;

namespace MyTemplateClean.Api.Apis;

public static class TodosApi
{
    public static RouteGroupBuilder MapTodosApiV1(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("api/todos").HasApiVersion(1.0);
        
        api.MapPost("/", CreateTodoAsync);
        api.MapDelete("/{id}", DeleteTodoAsync);
        api.MapGet("/", GetTodosAsync);
        api.MapGet("/by-title/{title}", GetTodosByTitleAsync);
        api.MapGet("/by-status/{status}", GetTodosByStatusAsync);
        return api;
    }

    public static async Task<Results<Ok, BadRequest<string>>> CreateTodoAsync(
        [FromHeader(Name = "x-requestid")] Guid requestId,
        CreateTodoRequest request,
        [AsParameters] TodoServices services
    )
    {
        if (requestId == Guid.Empty)
        {
            services.Logger.LogWarning("Invalid IntegrationEvent - RequestId is missing - {@IntegrationEvent}", request);
            return TypedResults.BadRequest("RequestId is missing.");
        }

        using (services.Logger.BeginScope(new List<KeyValuePair<string, object>>
                   { new("IdentifiedCommandId", requestId) }))
        {
            var createTodoCommand = new CreateTodoCommand(request.TodoDto);
            

            var result = await services.Mediator.Send(createTodoCommand);
            
            if (result.Id != Guid.Empty)
            {
                services.Logger.LogInformation("CreateOrderCommand succeeded - RequestId: {RequestId}", requestId);
            }
            else
            {
                services.Logger.LogWarning("CreateOrderCommand failed - RequestId: {RequestId}", requestId);
            }

            return TypedResults.Ok();

        }
        
    }
    
    public static async Task<Results<Ok, NotFound, BadRequest<string>>> DeleteTodoAsync(
        Guid id,
        [FromHeader(Name = "x-requestid")] Guid requestId,
        [AsParameters] TodoServices services
    )
    {
        if (requestId == Guid.Empty)
        {
            services.Logger.LogWarning("Invalid request - RequestId is missing for todo deletion - TodoId: {TodoId}", id);
            return TypedResults.BadRequest("RequestId is missing.");
        }

        using (services.Logger.BeginScope(new List<KeyValuePair<string, object>>
                   { new("IdentifiedCommandId", requestId) }))
        {
            var deleteTodoCommand = new DeleteTodoCommand(id);
            var result = await services.Mediator.Send(deleteTodoCommand);

            if (result.IsSuccess)
            {
                services.Logger.LogInformation("DeleteTodoCommand succeeded - RequestId: {RequestId}, TodoId: {TodoId}", requestId, id);
                return TypedResults.Ok();
            }

            services.Logger.LogWarning("DeleteTodoCommand failed - Todo not found - RequestId: {RequestId}, TodoId: {TodoId}", requestId, id);
            return TypedResults.NotFound();
        }
    }
    
    public static async Task<Results<Ok<PaginatedResult<TodoDto>>, BadRequest<string>>> GetTodosAsync(
        [AsParameters] PaginationRequest pagination,
        [AsParameters] TodoServices services
    )
    {
        var query = new GetTodosQuery(pagination);
        var result = await services.Mediator.Send(query);

        services.Logger.LogInformation("GetTodosQuery succeeded - Retrieved {Count} todos", result.Todos.Count);
        return TypedResults.Ok(result.Todos);
    }
    
    public static async Task<Results<Ok<IEnumerable<TodoDto>>, BadRequest<string>>> GetTodosByTitleAsync(
        string title,
        [AsParameters] TodoServices services
    )
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            services.Logger.LogWarning("Invalid request - Title is missing or empty");
            return TypedResults.BadRequest("Title cannot be empty.");
        }

        var query = new GetTodosByTitleQuery(title);
        var result = await services.Mediator.Send(query);

        services.Logger.LogInformation("GetTodosByTitleQuery succeeded - Title: {Title}, Retrieved {Count} todos", title, result.Todos.Count());
        return TypedResults.Ok(result.Todos);
    }

    public static async Task<Results<Ok<IEnumerable<TodoDto>>, BadRequest<string>>> GetTodosByStatusAsync(
        TodoStatus status,
        [AsParameters] TodoServices services
    )
    {
        if (string.IsNullOrWhiteSpace(status.ToString()))
        {
            services.Logger.LogWarning("Invalid request - Status is missing or empty");
            return TypedResults.BadRequest("Status cannot be empty.");
        }

        var query = new GetTodosByStatusQuery(status);
        var result = await services.Mediator.Send(query);

        services.Logger.LogInformation("GetTodosByStatusQuery succeeded - Status: {Status}, Retrieved {Count} todos", status, result.Todos.Count());
        return TypedResults.Ok(result.Todos);
    }
}

public record CreateTodoRequest(TodoDto TodoDto);