using System;
using System.Threading;
using Contracts;
using MassTransit;

namespace OrderApproval
{
    public class ApproveConsumer : Consumes<IApproveOrder>.All
    {
        private readonly IServiceBus bus;

        public ApproveConsumer(IServiceBus bus)
        {
            this.bus = bus;
        }

        public void Consume(IApproveOrder message)
        {
            Console.WriteLine("Approving order {0}", message.Text);
            Thread.Sleep(1000);
            if (true) {
                bus.Publish(new OrderApproved {Text = message.Text, CorrelationId = message.CorrelationId});
                return;
            }
            bus.Publish(new OrderRejected { Text = message.Text, CorrelationId = message.CorrelationId });
        }
    }
}
