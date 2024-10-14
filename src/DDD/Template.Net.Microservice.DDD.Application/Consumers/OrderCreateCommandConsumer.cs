using MassTransit;
using MediatR;
using Template.Net.Microservice.DDD.Application.Query;

namespace Template.Net.Microservice.DDD.Application.Consumers;

public class OrderCreateCommandConsumer : IConsumer<OrderCreateCommand>
{
    private readonly IMediator _mediator;

    public OrderCreateCommandConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<OrderCreateCommand> context)
    {
        var result = await _mediator.Send(context.Message, context.CancellationToken);
        await context.RespondAsync(result);
    }
}