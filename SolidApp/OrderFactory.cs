public class OrderFactory : IOrderFactory
{
    public Order CreateOrder(string customerName, decimal totalAmount)
    {
        return new Order
        {
            CustomerName = customerName,
            TotalAmount = totalAmount,
        };
    }
}