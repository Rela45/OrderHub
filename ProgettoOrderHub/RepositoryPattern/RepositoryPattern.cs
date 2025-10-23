using System;

#region REPOSITORY PATTERN

public record Product(Guid Id, string name, decimal Price);
public record Order(Guid Id, string Customer);
public interface IProductRepository
{
    IEnumerable<Product> GetAllProducts();
    void add(Product p);
    Product? Find(Guid id);
}

public interface IOrderRepository
{
    IEnumerable<Order> GetAll();
    void add(Order o);
    Order? Find(Guid id);
}

public class InMemoryProductRepository : IProductRepository
{
    private readonly List<Product> _products = new();
    public void add(Product p) => _products.Add(p);
    public IEnumerable<Product> GetAllProducts() => _products;
    public Product? Find(Guid id) => _products.FirstOrDefault(p => p.Id == id);
}

public class InMemoryOrderRepository : IOrderRepository
{
    private readonly List<Order> _orders = new();
    public void add(Order o) => _orders.Add(o);
    public IEnumerable<Order> GetAll() => _orders;
    public Order? Find(Guid id) => _orders.FirstOrDefault(o => o.Id == id);
}
#endregion
