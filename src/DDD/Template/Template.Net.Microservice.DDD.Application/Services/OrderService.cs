using Microsoft.EntityFrameworkCore;
using Pepegov.UnitOfWork;
using Pepegov.UnitOfWork.EntityFramework;
using Template.Net.Microservice.DDD.Application.Services.Interfaces;
using Template.Net.Microservice.DDD.Domain.Aggregate;
using Template.Net.Microservice.DDD.Infrastructure.Exceptions;

namespace Template.Net.Microservice.DDD.Application.Services;

public class OrderService : IOrderService
{
    private readonly IUnitOfWorkManager _unitOfWorkManager;

    public OrderService(IUnitOfWorkManager unitOfWorkManager)
    {
        _unitOfWorkManager = unitOfWorkManager;
    }

    public async Task<Order> GetOrderByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var unitOfWorkInstance = _unitOfWorkManager.GetInstance<IUnitOfWorkEntityFrameworkInstance>();
        unitOfWorkInstance.SetAutoDetectChanges(false);
        
        var orderRepository = unitOfWorkInstance.GetRepository<Order>();
        var order = (await orderRepository.GetFirstOrDefaultAsync(
            predicate: x => x.Id == id, 
            include: include => include.Include(i => i.Items).Include(i => i.Taxes), 
            cancellationToken: cancellationToken));

        if (order is null)
        {
            throw new NotFoundException($"Order by id {id} was not found");
        }

        return order;
    }
}