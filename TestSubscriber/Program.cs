using System;
using Autofac;
using Configuration;
using MassTransit;
using MassTransit.Saga;

namespace OrderSaga
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (
                BusInitializer.CreateBus("OrderSaga",
                    x => x.Subscribe(c => c.Saga(new InMemorySagaRepository<OrderSaga>()).Permanent())))
            {
                Console.WriteLine("Waiting..");
                Console.ReadKey();
            }
        }
    }
}
