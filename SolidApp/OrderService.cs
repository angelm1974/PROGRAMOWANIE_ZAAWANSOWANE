
public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;


    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    public void CreateOrder(string customerName, decimal totalAmount)
    {
       var order = new Order
        {
            
            CustomerName = customerName,
            TotalAmount = totalAmount,
            
        };
        _orderRepository.Add(order);
    }

    public Order GetOrderById(Guid id)
    {
        return _orderRepository.GetById(id);
    }

    public IEnumerable<Order> GetOrderByName(string name)
    {
        return _orderRepository.GetByName(name);
    }
}