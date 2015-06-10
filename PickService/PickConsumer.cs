using System;
using System.Threading;
using Contracts;
using MassTransit;

namespace PickService
{
    public class PickConsumer : Consumes<IPick>.All
    {
        private readonly IServiceBus bus;

        public PickConsumer(IServiceBus bus)
        {
            this.bus = bus;
        }

        public void Consume(IPick message)
        {
            Console.WriteLine("Pick order {0}", message.What);
            Thread.Sleep(1000);
            bus.Publish(new OrderPicked {CorrelationId = message.CorrelationId, Text = message.What});
        }
    }
}
