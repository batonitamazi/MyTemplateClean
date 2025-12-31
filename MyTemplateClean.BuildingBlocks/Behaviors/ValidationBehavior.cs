using FluentValidation;
using Mediator;

namespace MyTemplateClean.BuildingBlocks.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator> validators) : IPipelineBehavior<TRequest, TResponse> where TRequest: ICommand<TResponse>
{
    public async ValueTask<TResponse> Handle(TRequest request, MessageHandlerDelegate<TRequest, TResponse> next, CancellationToken cancellationToken)
    {

        var context = new ValidationContext<TRequest>(request);
        var validationResults = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));
        var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();
        if (failures.Any())
        {
            throw new ValidationException(failures);
        }
        return await next(request, cancellationToken);
    }
}