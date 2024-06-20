using OpenIddict.Server.AspNetCore;

namespace Template.Net.Microservice.DAL.Domain;

/// <summary>
/// Data for authorization
/// </summary>
public class AuthData
{
    /// <summary>
    /// Schemes for authorization filter
    /// </summary>
    public const string AuthenticationSchemes = OpenIddictServerAspNetCoreDefaults.AuthenticationScheme;
}