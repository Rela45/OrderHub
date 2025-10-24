using Domain;
using Singleton;
using DependencyInjection;
using RepositoryPattern;
using ProgettoOrderHub.MethodFactory;
class Program
{
    static void Main(string[] args)
    {
        IProductRepository productRepository = new InMemoryProductRepository();
        IOrderRepository orderRepository = new InMemoryOrderRepository();
        IConfigurationProvider configurationProvider = ConfigurationProvider.Instance;
        var orderservice = new OrderService(orderRepository, productRepository,configurationProvider);
        
        var mouse = new Product("MOU123", "Mouse", 25);
        var keyboard = new Product("KEY456", "Keyboard", 45);
        var screen = new Product("SCR789", "Screen", 155);
        productRepository.add(mouse);
        productRepository.add(keyboard);
        productRepository.add(screen);
        Order CurrentOrder = null;
        while (true)
        {
            Console.WriteLine("-----MENU-----");
            Console.WriteLine("1. List Products");
            Console.WriteLine("2. Create new Order");
            Console.WriteLine("3. Add product to current order");
            Console.WriteLine("4. Checkout");
            Console.WriteLine("5. ship current order");
            Console.WriteLine("6.List orders");
            Console.WriteLine("7.Cancel current order");
            Console.WriteLine("8. Exit");
            var choice = Console.ReadLine();
            if (choice == "1")
            {
                foreach (var p in productRepository.GetAllProducts())
                {
                    Console.WriteLine($"{p.Code} - {p.Name} - ${p.Price}");
                }
            }
            else if (choice == "2")
            {
                var orderId = Guid.NewGuid();
                Console.Write("Enter customer name: ");
                var customer = Console.ReadLine() ?? "Guest";
                CurrentOrder = orderservice.CreateOrder(orderId, 0, customer);
                Console.WriteLine($"Order created with ID: {orderId}");
            }
            else if (choice == "3")
            {
                if (CurrentOrder == null)
                {
                    Console.WriteLine("No current order. Please create an order first.");
                    continue;
                }
                Console.Write("Enter product code: ");
                var code = Console.ReadLine();
                var product = productRepository.Find(code ?? "");
                if (product == null)
                {
                    Console.WriteLine("Product not found.");
                    continue;
                }
                Console.Write("Enter quantity: ");
                var quantity = int.Parse(Console.ReadLine() ?? "1");
                orderservice.AddItem(CurrentOrder.Id, product, quantity);
                Console.WriteLine("Product added to order.");
            }
            else if (choice == "4")
            {
                if (CurrentOrder == null)
                {
                    Console.WriteLine("No current order. Please create an order first.");
                    continue;
                }
                Console.WriteLine("choose a payment type( Carta,Paypal,Googlepay");
                var paymentchoice = Console.ReadLine();
                ProgettoOrderHub.MethodFactory.

            }
            else if (choice == "5")
            {
                if (CurrentOrder == null)
                {
                    Console.WriteLine("No current order. Please create an order first.");
                    continue;
                }
                orderservice.ShipOrder(CurrentOrder.Id);
                Console.WriteLine("Order shipped.");
            }
            else if (choice == "6")
            {
                var orders = orderservice.AllOrders();
                foreach (var order in orders)
                {
                    Console.WriteLine($"Order ID: {order.Id}, Customer: {order.Customer}, Status: {order.Status}");
                }
            }
            else if (choice == "7")
            {
                if (CurrentOrder == null)
                {
                    Console.WriteLine("No current order. Please create an order first.");
                    continue;
                }
                orderservice.Cancel(CurrentOrder.Id);
                Console.WriteLine("Order cancelled.");
            }
            else if (choice == "8")
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid choice. Please try again.");
            }
        }

    }
}