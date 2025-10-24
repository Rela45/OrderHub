using Domain;
namespace RepositoryPattern
{

    #region REPOSITORY PATTERN
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();
        void add(Product p);
        Product? Find(string code);
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
        public Product? Find(string code) => _products.FirstOrDefault(p => p.Code == code);
    }

    public class InMemoryOrderRepository : IOrderRepository
    {
        private readonly List<Order> _orders = new();
        public void add(Order o) => _orders.Add(o);
        public IEnumerable<Order> GetAll() => _orders;
        public Order? Find(Guid id) => _orders.FirstOrDefault(o => o.Id == id);
    }
}
#endregion
