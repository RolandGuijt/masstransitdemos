using System;
using Contracts;

namespace OrderSender
{
    public class Order : IOrder
    {
        public string What { get; set; }
        public DateTime When { get; set; }
        public Guid CorrelationId { get; set; }
    }
}
