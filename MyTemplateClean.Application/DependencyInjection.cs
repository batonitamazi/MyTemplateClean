using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;
using MyTemplateClean.BuildingBlocks.Behaviors;
using Mediator;

namespace MyTemplateClean.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediator((MediatorOptions k) =>
        {
            k.Assemblies = new List<AssemblyReference>() { Assembly.GetExecutingAssembly() };
            k.PipelineBehaviors = [typeof(ValidationBehavior<,>), typeof(LoggingBehavior<,>)];
        });
        services.AddFeatureManagement();
        return services; 
    }
    
}