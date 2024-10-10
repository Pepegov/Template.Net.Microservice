using MediatR;
using Pepegov.MicroserviceFramework.ApiResults;
using Template.Net.Microservice.DDD.Application.Dtos;

namespace Template.Net.Microservice.DDD.Application.Query;

public record OrderCreateCommand(OrderCreationDto CreationDto) : IRequest<ApiResult>;