public interface IOrderFactory
{
    Order CreateOrder(string customerName, decimal totalAmount);
}