using Bookify.Application.Abstractions.Email;
using Bookify.Domain.Apartments;
using Bookify.Domain.Bookings;
using Bookify.Domain.Bookings.Events;
using Bookify.Domain.Users;
using MediatR;

namespace Bookify.Application.Bookings.ReserveBooking
{
    internal sealed class BookingReservedDomainEventHandler : INotificationHandler<BookingReservedDomainEvent>
    {
        private readonly IUserRepository _userRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IApartmentRepository _apartmentRepository;
        private readonly IEmailService _emailService;
        public async Task Handle(BookingReservedDomainEvent notification, CancellationToken cancellationToken)
        {
            var booking = await _bookingRepository.GetByIdAsync(notification.id);

            if (booking == null)
            {
                return;
            }

            var maybeUser = await _userRepository.GetByIdAsync(booking.UserId);

            if (maybeUser == null)
            {
                return;
            }

            var maybeApartment = await _apartmentRepository.GetByIdAsync(booking.ApartmentId);

            if (maybeApartment == null)
            {
                return;
            }

            await _emailService.SendEmailAsync(maybeUser.Email, "Booking Reserved!", "You need to confirm booking within 10 minutes");
        }
    }
}
