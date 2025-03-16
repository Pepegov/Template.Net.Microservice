namespace Template.Net.Microservice.DDD.Domain.ValueObject;

public class Money
{
    public decimal Amount { get; }
    public string Currency { get; }

    public Money(decimal amount, string currency)
    {
        Amount = amount;
        Currency = currency;
    }

    //The "Equals" and "GetHashCode" methods must be overrided for the value object
    
    public override bool Equals(object? obj)
    {
        if (obj is Money money)
        {
            return Amount == money.Amount && Currency == money.Currency;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Amount, Currency);
    }
}