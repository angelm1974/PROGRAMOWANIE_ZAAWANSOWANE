public class Order
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string ?CustomerName { get; set; }
    public decimal TotalAmount { get; set; }
}