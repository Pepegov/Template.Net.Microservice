namespace Template.Net.Microservice.ThreeTier.PL.Api.Definitions.MassTransit;

/// <summary>
/// App settings masstransit options
/// </summary>
public class MassTransitOption
{
    /// <summary>
    /// Domain or ip
    /// </summary>
    public string Url { get; set; } = null!;
    /// <summary>
    /// Virtual host
    /// </summary>
    public string Host { get; set; } = null!;
    /// <summary>
    /// Username
    /// </summary>
    public string User { get; set; } = null!;
    /// <summary>
    /// Password
    /// </summary>
    public string Password { get; set; } = null!;
}