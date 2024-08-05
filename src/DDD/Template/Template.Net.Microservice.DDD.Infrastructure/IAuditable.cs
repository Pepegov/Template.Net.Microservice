namespace Template.Net.Microservice.DDD.Infrastructure;

public interface IAuditable
{
    /// <summary>
    /// DateTime of creation. This value will never changed
    /// </summary>
    DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// DateTime of last value update. Should be updated when entity data updated
    /// </summary>
    DateTime? UpdatedAt { get; set; }
}