namespace Template.Net.Microservice.DDD.Domain.Entity;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; }
    public decimal Price { get; }
    public int Quantity { get;  }
}