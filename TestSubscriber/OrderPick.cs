using System;
using Contracts;

namespace OrderSaga
{
    public class OrderPick: IPick
    {
        public string What { get; set; }

        public Guid CorrelationId { get; set; }
    }
}
