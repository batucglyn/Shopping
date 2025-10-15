using EventBus.Messages.Events;
using MassTransit;
using MassTransit.Mediator;
using MassTransit.Transports;

namespace Payment.Consumer
{
    public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<OrderCreatedConsumer> _logger;
        public OrderCreatedConsumer( ILogger<OrderCreatedConsumer> logger, IPublishEndpoint endpoint)
        {           
            _logger = logger;
            _publishEndpoint = endpoint;
        }




        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        { 
            var message = context.Message; //OrderCreatedEvent nesnesini al
            _logger.LogInformation("Processing payment for Order Id: {OrderId}", message.Id);

            //Simulate for Payment processing
            await Task.Delay(1000);
            if (message.TotalPrice > 0)
            {
                //Simulating Success
                var completedEvent = new PaymentCompletedEvent
                {
                    OrderId = message.Id,
                    CorrelationId = context.CorrelationId ?? Guid.NewGuid()
                };
                await _publishEndpoint.Publish(completedEvent);
                _logger.LogInformation("Payment success for Order Id: {OrderId}", message.Id);
            }
            else
            {
                var failedEvent = new PaymentFailedEvent
                {
                    OrderId = message.Id,
                    CorrelationId = context.CorrelationId ?? Guid.NewGuid(),
                    Reason = "Total price was zero or negative."
                };
                await _publishEndpoint.Publish(failedEvent);
                _logger.LogWarning("Payment failed for Order Id: {OrderId}", message.Id);
            }
        }
    }
}
