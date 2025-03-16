using Template.Net.Microservice.DDD.Domain.Aggregate;

namespace Template.Net.Microservice.DDD.Domain.Service;

public class OrderDomainService
{
    public bool CanOrderBeShipped(Order order)
    {
        return order.CreationDate <= DateTime.Now;
    }
}