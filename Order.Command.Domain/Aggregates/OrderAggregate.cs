using CQRS.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Command.Domain.Aggregates
{
    public class OrderAggregate : AggregateRoot
    {
        private bool _active;
    }
}
