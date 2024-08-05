using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pepegov.MicroserviceFramework.AspNetCore.WebApplicationDefinition;
using Pepegov.MicroserviceFramework.Definition;
using Pepegov.MicroserviceFramework.Definition.Context;
using Pepegov.MicroserviceFramework.Infrastructure.Attributes;
using Serilog;

namespace Template.Net.Microservice.DDD.UI.Api.EndPoints;

/// <summary>
/// Mapping claim endpoints
/// </summary>
public class ClaimsEndPoint : ApplicationDefinition
{
    /// <inheritdoc />
    public override Task ConfigureApplicationAsync(IDefinitionApplicationContext context)
    {
        var app = context.Parse<WebDefinitionApplicationContext>().WebApplication;
        app.MapGet("~/api/claims", GetClaims).WithOpenApi().WithTags("Claims");

        return base.ConfigureApplicationAsync(context);
    }
    
    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    [FeatureGroupName("Claims")]
    [Authorize(AuthenticationSchemes = AuthData.AuthenticationSchemes)]
    private IResult GetClaims( 
        [FromServices] IHttpContextAccessor httpContextAccessor)
    {
        var user = httpContextAccessor.HttpContext!.User;
        var claims = ((ClaimsIdentity)user.Identity!).Claims;
        var result = claims.Select(x => new { Type = x.Type, ValueType = x.ValueType, Value = x.Value }).ToList();
        Log.Information($"Current user {user.Identity.Name} have following climes {result}");
        return Results.Ok(result);
    }
}