using Bookify.Domain.Abstraction;

namespace Bookify.Domain.Bookings.Events
{
    public record BookingConfirmedDomainEvent(Guid Id) : IDomainEvent
    {
    }
}
