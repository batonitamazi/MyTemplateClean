using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;
using MyTemplateClean.BuildingBlocks.Behaviors;
using Mediator;
using MyTemplateClean.BuildingBlocks.Messaging.MassTransit;

namespace MyTemplateClean.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediator((MediatorOptions k) =>
        {
            k.Assemblies = new List<AssemblyReference>() { Assembly.GetExecutingAssembly() };
            k.PipelineBehaviors = [typeof(ValidationBehavior<,>), typeof(LoggingBehavior<,>)];
        });
        services.AddFeatureManagement();
        services.AddMessageBroker(configuration, Assembly.GetExecutingAssembly());
        return services; 
    }
    
}