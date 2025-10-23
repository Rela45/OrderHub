using Singleton;

namespace Domain
{
    
    #region Entities
    public enum OrderStatus { New, Paid, Shipped, Cancelled }
    public record Product(string Code, string Name, decimal Price);

    public record OrderItem(Product Product, int Quantity)
    {
        public decimal LineTotal => Product.Price * Quantity;
    }

    public class Order
    {
        public int Id { get; set; }
        public decimal Prezzo { get; set; }
        public string? Customer { get; set; }

        // private readonly IConfigurationProvider configurationProvider;


        public OrderStatus Status { get; private set; } = OrderStatus.New;
        private readonly List<OrderItem> _items = new();

        public Order(string customer)
        {
            Customer = customer;
        }

        
        public void AddItems(Product p, int quantity)
        {
            if (Status != OrderStatus.New) throw new InvalidOperationException("Puoi aggiungere un ordine soltanto in stato NEW");
            if (quantity <= 0) throw new ArgumentException("La quantita deve essere maggiore di 0");
            _items.Add(new OrderItem(p, quantity));
        }

        

        public void Pay()
        {
            if (Status != OrderStatus.New) { Console.WriteLine($"Puoi pagare solo ordini nuovi"); }
            Status = OrderStatus.Paid;
        }

        public void Ship()
        {
            if (Status != OrderStatus.Paid)
            {
                Console.WriteLine($"Solo ordini pagati possono essere spediti");
            }
            Status = OrderStatus.Shipped;
        }
        public void Cancel()
        {
            if (Status != OrderStatus.Shipped)
            {
                Console.WriteLine("Non puoi annullare un ordine gia spedito");
            }
            Status = OrderStatus.Cancelled;
        }

        public decimal SubTotal() => _items.Sum(i => i.LineTotal);

        public decimal Total(IConfigurationProvider configurationProvider)
        {
            decimal iva = configurationProvider.TaxRate;
            return SubTotal() + iva;
        }

    } 

    #endregion

}