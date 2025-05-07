using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using Ordering.Application.Extensions;
using Ordering.Domain.Events;

namespace Ordering.Application.Orders.EventHandlers.Domain
{
    public class OrderCreatedEventHandler(IPublishEndpoint publishEndpoint, IFeatureManager featureManager, ILogger<OrderCreatedEventHandler> logger) : INotificationHandler<OrderCreatedEvent>
    {
        public async Task Handle(OrderCreatedEvent domainEvent, CancellationToken cancellationToken)
        {
            logger.LogInformation("Domain Event handler: {DomainEvent}", domainEvent.GetType().Name);
            
            if(await featureManager.IsEnabledAsync("OrderFullfilment"))
            {
                OrderDto integrationEvent = domainEvent.Order.ToOrderDto();

                await publishEndpoint.Publish(integrationEvent, cancellationToken);
            }
        }
    }
}
