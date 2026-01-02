using System.Reflection;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MyTemplateClean.BuildingBlocks.Messaging.MassTransit;

public static class Extensions
{
    public static IServiceCollection AddMessageBroker(
        this IServiceCollection services,
        IConfiguration configuration,
        Assembly? assembly = null)
    {
        var host = configuration["MessageBroker:Host"] 
                   ?? throw new InvalidOperationException("MessageBroker:Host is required");
        var userName = configuration["MessageBroker:UserName"] 
                       ?? throw new InvalidOperationException("MessageBroker:UserName is required");
        var password = configuration["MessageBroker:Password"] 
                       ?? throw new InvalidOperationException("MessageBroker:Password is required");
        
        services.AddMassTransit(config =>
        {
            config.SetKebabCaseEndpointNameFormatter();
            
            if (assembly != null)
                config.AddConsumers(assembly);
            
            config.UsingRabbitMq((context, configurator) =>
            {
                configurator.Host(host, h =>
                {
                    h.Username(userName);
                    h.Password(password);
                });
                configurator.ConfigureEndpoints(context);
            });
        });
        
        return services;
    }
}