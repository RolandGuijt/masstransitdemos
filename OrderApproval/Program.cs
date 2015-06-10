using System;
using Autofac;
using Configuration;
using MassTransit;

namespace OrderApproval
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ApproveConsumer>().AsSelf();

            builder.Register(c => ServiceBusFactory.New(sbc =>
            {
                sbc.UseRabbitMq();
                sbc.ReceiveFrom(BusInitializer.GetUri("OrderApprover"));

                sbc.SetConcurrentConsumerLimit(5);
                sbc.SetDefaultRetryLimit(5);

                //this will find all of the consumers in the container and
                //register them with the bus.
                sbc.Subscribe(x => x.LoadFrom(c.Resolve<ILifetimeScope>()));
            })).As<IServiceBus>().SingleInstance().AutoActivate();

            builder.Build();

            Console.WriteLine("Waiting..");
            Console.ReadKey();
        }
    }
}
