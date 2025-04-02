public interface IOrderService
{
void CreateOrder(
    string customerName, decimal totalAmount);
    Order GetOrderById(Guid id);

    IEnumerable<Order> GetOrderByName(string name);
}