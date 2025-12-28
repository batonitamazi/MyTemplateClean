namespace MyTemplateClean.Domain.ValueObjects;

public class TodoId
{
    public Guid Value { get; }

    private TodoId(Guid value) => Value = value;
    
    public static TodoId Of(Guid todoId)
    {
        ArgumentNullException.ThrowIfNull(todoId);
        return new TodoId(todoId);
    }
}