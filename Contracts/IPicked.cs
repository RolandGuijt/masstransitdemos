using System;
using MassTransit;

namespace Contracts
{
    public interface IPicked: CorrelatedBy<Guid>
    {
        string Text { get; set; }
    }
}