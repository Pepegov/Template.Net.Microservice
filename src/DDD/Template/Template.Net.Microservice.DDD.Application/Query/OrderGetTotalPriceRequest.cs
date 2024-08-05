using MediatR;
using Pepegov.MicroserviceFramework.ApiResults;

namespace Template.Net.Microservice.DDD.Application.Query;

public record OrderGetTotalPriceRequest(Guid Id) : IRequest<ApiResult<decimal>>;