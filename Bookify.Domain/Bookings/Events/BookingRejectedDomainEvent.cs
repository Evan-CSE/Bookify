using Bookify.Domain.Abstraction;

namespace Bookify.Domain.Bookings.Events
{
    public record BookingRejectedDomainEvent(Guid Id) : IDomainEvent
    {
    }
}
