using System;
using Configuration;

namespace OrderSender
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var bus = BusInitializer.CreateBus("OrderSender", x => { }))
            {
                var text = "";

                while (text != "quit")
                {
                    Console.Write("Enter an order: ");
                    text = Console.ReadLine();

                    var message = new Order {What = text, When = DateTime.Now, CorrelationId = Guid.NewGuid()};
                    var receiverUri = BusInitializer.GetUri("OrderSaga");

                    bus.GetEndpoint(receiverUri)
                        .Send(message);
               }
            }
        }
    }
}
