using System;
using MassTransit;

namespace Contracts
{
    public interface IOrderRejected : CorrelatedBy<Guid>
    {
        string Text { get; set; } 
    }
}