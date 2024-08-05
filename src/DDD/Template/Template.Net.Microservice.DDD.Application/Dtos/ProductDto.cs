namespace Template.Net.Microservice.DDD.Application.Dtos;

public class ProductDto
{
    public string Name { get; set; } = null!;
    public int Quality { get; set; }
    public decimal Price { get; set; }
}