using System;
using MassTransit;

namespace Contracts
{
    public interface IPick : CorrelatedBy<Guid>
    {
         string What { get; set; }
    }
}