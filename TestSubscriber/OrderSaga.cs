using System;
using Contracts;
using Magnum.StateMachine;
using MassTransit;
using MassTransit.Saga;

namespace OrderSaga
{
    public class OrderSaga: SagaStateMachine<OrderSaga>, ISaga
    {
        static OrderSaga()
        {
            Define(() =>
            {
                Initially(
                    When((Create))
                        .Then((saga, message) => Console.WriteLine("{0}: Order created, sending approval request", message.What))
                        .Publish((saga, message) => new ApproveOrder { Text = message.What, CorrelationId = saga.CorrelationId })
                        .TransitionTo(Approve));
                During(Approve,
                    When(Approved)
                        .Then((saga, message) => Console.WriteLine("{0}: Approval received, sending pick request", message.Text))
                        .Publish((saga, message) => new OrderPick { What = message.Text, CorrelationId = saga.CorrelationId })
                        .TransitionTo(Pick), 
                    When(Rejected)
                        .Complete());
                During(Pick,
                    When(Picked)
                        .Then((saga, message) => Console.WriteLine("{0}: Picking complete, workflow ends", message.Text))
                        .Complete());
            });
        }

        public OrderSaga(Guid correlationId)
        {
            CorrelationId = correlationId;
        }

        public Guid CorrelationId { get; set; }
        public IServiceBus Bus { get; set; }

        public static State Initial { get; set; }
        public static State Approve { get; set; }
        public static State Pick { get; set; }
        public static State Completed { get; set; }

        public static Event<IOrder> Create { get; set; }
        public static Event<IOrderApproved> Approved { get; set; }
        public static Event<IOrderRejected> Rejected { get; set; }
        public static Event<IPicked> Picked { get; set; }
    }
}
