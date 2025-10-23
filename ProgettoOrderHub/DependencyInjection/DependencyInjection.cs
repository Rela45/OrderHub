using Domain;
using Singleton;

#region DEPENDENCY INJECTION
public class OrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IConfigurationProvider _configurationProvider;
    public OrderService(
        IOrderRepository orderRepository,
        IProductRepository productRepository,
        IConfigurationProvider configurationProvider)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _configurationProvider = configurationProvider;
    }
    public Order CreateOrder(Guid id, decimal price, string customer)
    {
        var order = new Order(id, price, customer);
        _orderRepository.add(order);
        return order;
    }
    public void AddItem(Guid id, Product product, int quantity)
    {
        var order = _orderRepository.Find(id);
        if (order == null) return;
        order.AddItems(product, quantity);
    }

    public decimal Checkout(Guid orderId)   //IPayment
    {
        var order = _orderRepository.Find(orderId);
        if (order == null) return 0;
        decimal total = order.Total(_configurationProvider);
        order.Pay();
        return total;
    }
    public void ShipOrder(Guid orderId)
    {
        var order = _orderRepository.Find(orderId);
        if (order == null )return;
        if(order.Status== OrderStatus.Paid)
        {
            order.Ship();
        }
    }

    public void Cancel (Guid orderId)
    {
        var order= _orderRepository.Find(orderId);
        order?.Cancel();
    }
    public IEnumerable<Order> AllOrders() => _orderRepository.GetAll();
}
#endregion