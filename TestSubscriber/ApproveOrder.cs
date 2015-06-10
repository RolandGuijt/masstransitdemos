using System;
using Contracts;

namespace OrderSaga
{
    public class ApproveOrder: IApproveOrder
    {
        public string Text { get; set; }
        public Guid CorrelationId { get; set; }
    }
}
