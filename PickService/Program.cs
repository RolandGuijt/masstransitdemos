using System;
using Autofac;
using Configuration;
using MassTransit;

namespace PickService
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();

            // register each consumer manually
            builder.RegisterType<PickConsumer>().AsSelf();

            //or use Autofac's scanning capabilities -- SomeClass is any class in the correct assembly
            //builder.RegisterAssemblyTypes(typeof(PickConsumer).Assembly)
            //    .Where(t => t.Implements<IConsumer>())
            //    .AsSelf();

            //now we add the bus
            builder.Register(c => ServiceBusFactory.New(sbc =>
            {
                sbc.UseRabbitMq();
                sbc.ReceiveFrom(BusInitializer.GetUri("PickService"));

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
