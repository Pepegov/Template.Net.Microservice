using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pepegov.MicroserviceFramework.AspNetCore.WebApi;
using Pepegov.MicroserviceFramework.AspNetCore.WebApplicationDefinition;
using Pepegov.MicroserviceFramework.Definition;
using Pepegov.MicroserviceFramework.Definition.Context;
using Template.Net.Microservice.DDD.Application.Dtos;
using Template.Net.Microservice.DDD.Application.Query;

namespace Template.Net.Microservice.DDD.UI.Api.EndPoints;

public class OrderEndPoints : ApplicationDefinition
{
    public override Task ConfigureApplicationAsync(IDefinitionApplicationContext context)
    {
        var app = context.Parse<WebDefinitionApplicationContext>().WebApplication;
        app.MapGet("~/api/order/{id}/price", GetTotalPrice).WithOpenApi().WithTags("Order");
        app.MapPost($"~/api/order/create", CreateOrder).WithOpenApi().WithTags("Order");
            
        return base.ConfigureApplicationAsync(context);
    }

    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    [ProducesResponseType(500)]
    [Authorize(AuthenticationSchemes = AuthData.AuthenticationSchemes)]
    private async Task<IResult> GetTotalPrice(
        HttpContext httpContext,
        Guid id,
        [FromServices] IMediator mediator)
    {
        var result = await mediator.Send(new OrderGetTotalPriceRequest(id), httpContext.RequestAborted);
        return Results.Extensions.Custom(result);
    }

    private async Task<IResult> CreateOrder(
        HttpContext httpContext,
        [FromBody] OrderCreationDto creationModel,
        [FromServices] IMediator mediator)
    {
        var result = await mediator.Send(new OrderCreateCommand(creationModel), httpContext.RequestAborted);
        return Results.Extensions.Custom(result);   
    }
}