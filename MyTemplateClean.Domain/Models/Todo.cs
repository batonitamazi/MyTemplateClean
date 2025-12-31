using MyTemplateClean.Domain.Abstractions;
using MyTemplateClean.Domain.Enums;
using MyTemplateClean.Domain.Events;
using MyTemplateClean.Domain.ValueObjects;

namespace MyTemplateClean.Domain.Models;

public class Todo : Aggregate<TodoId>
{
    public TodoTitle Title { get; private set; }
    public TodoStatus Status { get; private set; } = TodoStatus.Pending;
    
    public DateTime PublishDate { get; private set; } = DateTime.Now;
    
    public DateTime CompleteDate { get; private set; } = DateTime.Now;
    
    public bool IsDeleted { get; private set; } = false;


    public static Todo Create(TodoId id, TodoTitle title)
    {
        var toDo = new Todo()
        {
            Id = id,
            Title = title,
            PublishDate = DateTime.Now,
            Status = TodoStatus.Pending
        };
        
        toDo.AddDomainEvent(new TodoCreatedEvent(toDo));
        return toDo;
    }
    
    public void Update(TodoStatus status)
    {
        Status = status;
    }
    
    public void Delete()
    {
        if (IsDeleted) return; // optional guard
        IsDeleted = true;

        // Optional: raise domain event
        AddDomainEvent(new TodoDeletedEvent(this));
    }
}