using System;
using MassTransit;

namespace Contracts
{
    public interface IApproveOrder: CorrelatedBy<Guid>
    {
        string Text { get; set; } 
    }
}