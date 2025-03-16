using Template.Net.Microservice.DDD.Infrastructure;

namespace Template.Net.Microservice.DDD.Domain.Event;

public class OrderCreatedEvent(Guid orderId, DateTime creationDate) : IEvent
{
    public Guid OrderId { get; } = orderId;
    public DateTime CreationDate { get; } = creationDate;
}