#region DEPENDENCY INJECTION
public class OrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    public OrderService(IOrderRepository orderRepository, IProductRepository productRepository)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
    }
    public Prder CreateOrder(string customer)
    {
        var order = new OrderService(customer);
        _orderRepository.add(order);
        return order;
    }
    public void AddItem(Guid orderId, guid productId, int quantity)
    {
        var order= _orderRepository.Find(orderId);
        if (order == null) return;
        order.AddItem(new OrderItem(productId, quantity));
    }
    public decimal GetSubtotal(Order order)
    {
        return order.Items.Sum(item =>
        {
           _productRepository.Find(item.ProductId)?.Price * item.Quantity ?? 0;
        });
    }
    public decimal Checkout(Guid orderId, Ipayment Payment)
    {
        var order = _orderRepository.Find(orderId);
        if (order == null) return 0;
        decimal subtotal = GetSubtotal(order);
        decimal taxRate = ConfigurationProvider.Instance.Taxrate;
        decimal tax = subtotal * taxRate;
        decimal total = subtotal + tax;
        Payment.pay(order, totali);
        order.MarkPaid();
        OnOrderPaid?.Invoke(order.id, total);
        return total;
    }
    public void ShipOrder(Guid orderId)
    {
        var order = _orderRepository.Find(orderId);
        if (order == null )return;
        if(order.Status== orderStatus.paid)
        {
            order.MarkShipped();
            decimal total = GetSubtotal(order) * (1 + ConfigurationProvider.Instance.Taxrate);
            OnOrderShipped?.Invoke(order.Id, total);
        }
            order.MarkShipped();
        OnOrderShipped?.Invoke(order.Id);
    }
    public void Cancel (Guid orderId)
    {
        var order= _orderRepository.Find(orderId);
        order?.Cancel();
    }
    public system.collections.Generic.IEnumerable<OrderService> AllOrders() => _orderRepository.GetAll();

}
#endregion