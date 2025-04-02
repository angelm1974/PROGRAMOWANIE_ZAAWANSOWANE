public interface IOrderRepository
{
    void Add(Order order);
    Order GetById(Guid id);
    IEnumerable<Order> GetAll();
    IEnumerable<Order> GetByName(string customerName);
}