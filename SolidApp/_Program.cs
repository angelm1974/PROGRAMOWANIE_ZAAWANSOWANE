// internal class Program
// {
//     private static void Main(string[] args)
//     {
//         //Wstrzkniecie zaleznosci
//         IOrderRepository repository = new OrderRepository();
//         IOrderService orderService = new OrderService(repository);
//         IOrderFactory orderFactory = new OrderFactory();


//         var order = orderFactory.CreateOrder("Gryfnie",1000);
//         repository.Add(order);


//         orderService.CreateOrder("Geszynki", 2000);

//         var retriveOrders = orderService.GetOrderByName("g");
//         foreach (var item in retriveOrders)
//         {
//             Console.WriteLine(item.CustomerName + " " + item.Id + " " + item.TotalAmount);
//         }
//     }
// }