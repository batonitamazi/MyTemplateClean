using MyTemplateClean.Domain.Abstractions;
using MyTemplateClean.Domain.Enums;
using MyTemplateClean.Domain.Events;
using MyTemplateClean.Domain.ValueObjects;

namespace MyTemplateClean.Domain.Models;

public class Todo : Aggregate<TodoId>
{
    public TodoTitle Title { get; private set; }
    public TodoStatus Status { get; private set; } = TodoStatus.Pending;

    public static Todo Create(TodoId id, TodoTitle title)
    {
        var toDo = new Todo()
        {
            Id = id,
            Title = title,
            Status = TodoStatus.Pending
        };
        
        toDo.AddDomainEvent(new TodoCreatedEvent(toDo));
        return toDo;
    }
    
    public void Update(TodoStatus status)
    {
        Status = status;
    }
}