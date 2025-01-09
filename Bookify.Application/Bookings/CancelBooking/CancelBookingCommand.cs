using Bookify.Application.Abstractions.Messaging;

namespace Bookify.Application.Bookings.CancelBooking
{
    public sealed record CancelBookingCommand(
        Guid bookingId,
        Guid userId
    ) : ICommand<bool>;
}
