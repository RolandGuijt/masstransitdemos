using System;
using MassTransit;

namespace Contracts
{
    public interface IOrderApproved: CorrelatedBy<Guid>
    {
        string Text { get; set; }
    }
}