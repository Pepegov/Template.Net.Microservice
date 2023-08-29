namespace MicroserviceTemplate.PL.Definitions.Options.Models;

public class IdentityClientOption
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Secret { get; set; } = null!;
    public string? ConsentType { get; set; }
    public List<string>? GrandTypes { get; set; }
    public List<string>? Scopes { get; set; }
}