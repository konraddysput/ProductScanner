using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ProductScanner.Gateway.Cache;
using ProductScanner.Gateway.EventBus;
using ProductScanner.Gateway.Events;
using ProductScanner.Gateway.Handlers;
using ProductScanner.Gateway.Interfaces;
using RabbitMQ.Client;

namespace ProductScanner.Gateway.Configuration
{
    public static class EventBusConfiguration
    {
        public static IServiceCollection RegisterEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();

                var factory = new ConnectionFactory()
                {
                    HostName = configuration["RabbitMq:Hostname"],
                    UserName = configuration["RabbitMq:Username"],
                    Password = configuration["RabbitMq:Password"]
                };
                var retryCount = int.Parse(configuration["RabbitMq:RetryCount"]);

                return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
            });

            var subscriptionClientName = configuration["RabbitMq:SubscriptionName"];

            services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
            {
                var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
                var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                var retryCount = int.Parse(configuration["RabbitMq:RetryCount"]);
                return new EventBusRabbitMQ(rabbitMQPersistentConnection, logger, iLifetimeScope, eventBusSubcriptionsManager, subscriptionClientName, retryCount);
            });

            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
            services.AddTransient<ImageClasificationEventHandler>();
            return services;
        }


        public static IApplicationBuilder AddEventHandlers(this IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();

            eventBus.Subscribe<ImageClasificationIntegrationEvent, ImageClasificationEventHandler>();
            return app;
        }
    }
}
