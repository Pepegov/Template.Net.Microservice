namespace Template.Net.Microservice.ThreeTier.PL.Definitions.Identity.Options;

public class IdentityClientOption
{
    /// <summary>
    /// ClientId
    /// </summary>
    public string Id { get; set; } = null!;
    /// <summary>
    /// DisplayName
    /// </summary>
    public string Name { get; set; } = null!;
    /// <summary>
    /// ClientSecret
    /// </summary>
    public string Secret { get; set; } = null!;
    /// <summary>
    /// ConsentType
    /// </summary>
    public string? ConsentType { get; set; }
    /// <summary>
    /// List of grand type
    /// </summary>
    public List<string>? GrandTypes { get; set; }
    /// <summary>
    /// List of scopes
    /// </summary>
    public List<string>? Scopes { get; set; }
}