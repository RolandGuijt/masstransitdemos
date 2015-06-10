using System;
using MassTransit;

namespace Contracts
{
  public interface IOrder: CorrelatedBy<Guid>
  {
    string What { get; }
    DateTime When { get; }
  }
}
