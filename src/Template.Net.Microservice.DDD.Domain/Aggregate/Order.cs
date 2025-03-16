using Template.Net.Microservice.DDD.Domain.Entity;
using Template.Net.Microservice.DDD.Infrastructure;

namespace Template.Net.Microservice.DDD.Domain.Aggregate;

public class Order : IAggregateRoot
{
    public Guid Id { get; }
    public DateTime CreationDate { get; }
    private List<Product> _items;
    private List<Tax> _taxes;
    
    public IReadOnlyCollection<Product> Items => _items.AsReadOnly();
    public IReadOnlyCollection<Tax> Taxes => _taxes.AsReadOnly();

    public Order(Guid id)
    {
        Id = id;
        CreationDate = DateTime.UtcNow;
        _items = new List<Product>();
        _taxes = new List<Tax>();
    }

    public void AddProduct(Product product)
    {
        // The Agregate can determine whether it is possible to add such a product
        if (!CanAddProduct(product))
        {
            return;
        }
        _items.Add(product);
        // recalculating taxes and the total cost
        RecalculateTaxesAndTotalPrice();
    }

    public decimal GetTaxesAmount()
    {
        return _taxes.Sum(x => x.Amount);
    }

    public decimal GetTotalPrice()
    {
        return _items.Sum(item => item.Price * item.Quantity) + _taxes.Sum(tax => tax.Amount);
    }
    
    private void RecalculateTaxesAndTotalPrice(int taxPercent = 0)
    {
        if (taxPercent != 0)
        {
            _taxes.ForEach(tax => tax.DecreaseByPercentage(taxPercent));
        }
    }

    private bool CanAddProduct(Product product)
    {
        //some checks
        return true;
    }
}