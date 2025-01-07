using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookify.Domain.Abstraction;

namespace Bookify.Domain.Review.Events
{
    public sealed record ReviewCreatedDomainEvent(Guid id) : IDomainEvent
    {
    }
}
