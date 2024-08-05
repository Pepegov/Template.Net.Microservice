namespace Template.Net.Microservice.DDD.Application.Dtos;

public class OrderCreationDto
{
    public OrderCreationDto(Guid id,  List<ProductDto> products)
    {
        Id = id;
        Products = products;
    }

    public Guid Id { get; }
    public List<ProductDto> Products { get; set; }
}