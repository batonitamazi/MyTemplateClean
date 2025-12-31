namespace MyTemplateClean.Application.Dtos;

public sealed record TodoDto(
    Guid Id,
    string Title,
    TodoStatus Status,
    DateTime CreatedAtUtc,
    DateTime? CompletedAtUtc
);