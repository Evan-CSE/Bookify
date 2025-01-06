using Bookify.Domain.Abstraction;

namespace Bookify.Domain.Bookings.Events
{
    public record IBookingCompletedEvent(DateTime utcNow) : IDomainEvent
    {
    }
}
