namespace MicroserviceTemplate.BL;

public interface IAuditable
{
    public bool IsDelete { get; set; }
    public DateTime UpdateAt { get; set; }
    public DateTime CreateAt { get; set; }
}