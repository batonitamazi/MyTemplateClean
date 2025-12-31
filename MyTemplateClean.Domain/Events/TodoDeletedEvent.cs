using MyTemplateClean.Domain.Abstractions;
using MyTemplateClean.Domain.Models;

namespace MyTemplateClean.Domain.Events;

public record TodoDeletedEvent(Todo todo) : IDomainEvent;