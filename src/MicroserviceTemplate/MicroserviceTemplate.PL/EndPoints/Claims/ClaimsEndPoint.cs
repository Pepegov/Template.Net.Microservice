using System.Security.Claims;
using MicroserviceTemplate.DAL.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pepegov.MicroserviceFramework.AspNetCore.WebApplicationDefinition;
using Pepegov.MicroserviceFramework.Definition;
using Pepegov.MicroserviceFramework.Definition.Context;
using Pepegov.MicroserviceFramework.Infrastructure.Attributes;
using Serilog;

namespace MicroserviceTemplate.PL.EndPoints.Claims;

public class ClaimsEndPoint : ApplicationDefinition
{
    public override async Task ConfigureApplicationAsync(IDefinitionApplicationContext context)
    {
        var webContext = context.Parse<WebDefinitionApplicationContext>();
        webContext.WebApplication.MapGet("~/api/claims/get", GetClaims).WithOpenApi();
    }
    
    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    [FeatureGroupName("Claims")]
    [Authorize(AuthenticationSchemes = AuthData.AuthenticationSchemes)]
    private async Task<IResult> GetClaims( 
        [FromServices] IHttpContextAccessor httpContextAccessor)
    {
        var user = httpContextAccessor.HttpContext!.User;
        var claims = ((ClaimsIdentity)user.Identity!).Claims;
        var result = claims.Select(x => new { Type = x.Type, ValueType = x.ValueType, Value = x.Value });
        Log.Information($"Current user {user.Identity.Name} have following climes {result}");
        return Results.Ok(result);
    }
}