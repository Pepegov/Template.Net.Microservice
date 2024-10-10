namespace Template.Net.Microservice.DDD.Domain.Entity;

public class Tax
{
    public Guid Id { get; set; }
    public decimal Amount { get; private set; }

    public void DecreaseByPercentage(int percent)
    {
        var dec = Amount / 100 * percent;
        this.Amount = Amount - dec;
    }
}