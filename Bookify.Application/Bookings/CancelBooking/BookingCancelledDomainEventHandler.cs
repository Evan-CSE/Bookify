using Bookify.Application.Abstractions.Email;
using Bookify.Domain.Bookings;
using Bookify.Domain.Bookings.Events;
using Bookify.Domain.Users;
using MediatR;

namespace Bookify.Application.Bookings.CancelBooking
{
    public sealed class BookingCancelledDomainEventHandler : INotificationHandler<BookingCancelledDomainEvent>
    {
        private readonly IEmailService _emailService;
        private readonly IBookingRepository _bookingRepository;
        private readonly IUserRepository _userRepository;
        public async Task Handle(BookingCancelledDomainEvent notification, CancellationToken cancellationToken)
        {
            var maybeBooking = await _bookingRepository.GetByIdAsync(notification.Id);

            if (maybeBooking == null)
            {
                return;
            }

            var maybeUser = await _userRepository.GetByIdAsync(maybeBooking.UserId);

            if (maybeUser == null)
            {
                return;
            }

            await _emailService.SendEmailAsync(maybeUser.Email, "Booking Cancellation Successful", "Your booking is cancelled. Contact with us if you think its a mistake from us");
        }
    }
}
