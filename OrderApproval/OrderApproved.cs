using System;
using Contracts;

namespace OrderApproval
{
    class OrderApproved: IOrderApproved
    {
        public string Text { get; set; }
        public Guid CorrelationId { get; set; }
    }
}
