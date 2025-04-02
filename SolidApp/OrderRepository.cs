
public class OrderRepository : IOrderRepository
{
    private readonly List<Order> _orders = new List<Order>();
    public void Add(Order order)
    {
        _orders.Add(order);
    }

    public IEnumerable<Order> GetAll()
    {
        return _orders;
    }

    public IEnumerable<Order> GetByName(string name)
    {
        return _orders.Where(o => o.CustomerName.StartsWith(name, StringComparison.OrdinalIgnoreCase));
    }

    public Order GetById(Guid id)
    {
        return _orders.FirstOrDefault(o => o.Id == id);
    }
}