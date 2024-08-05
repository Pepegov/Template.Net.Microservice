using Template.Net.Microservice.DDD.Domain.Aggregate;

namespace Template.Net.Microservice.DDD.Application.Services.Interfaces;

public interface IOrderService
{
    Task<Order> GetOrderByIdAsync(Guid id, CancellationToken cancellationToken = default);
}