using Bookify.Application.Abstractions.Messaging;
using Bookify.Domain.Abstraction;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Apartments;
using Bookify.Domain.Bookings;
using Bookify.Domain.Users;

namespace Bookify.Application.Bookings.ReserveBooking
{
    internal sealed class ReserveBookingCommandHandler : ICommandHandler<ReserveBookingCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IApartmentRepository _apartmentRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly PricingService _pricingService;
        private readonly IUnitofWork _unitofWork;

        public async Task<Result<Guid>> Handle(ReserveBookingCommand command, CancellationToken cancellationToken)
        {
            var maybeUser = await _userRepository.GetByIdAsync(command.UserId);

            if (maybeUser is null)
            {
                return Result.Failure<Guid>(UserErrors.UserNotFound);
            }

            Apartment maybeApartment = await _apartmentRepository.GetByIdAsync(
                command.ApartmentId,
                cancellationToken);

            if (maybeApartment is null)
            {
                return Result.Failure<Guid>(ApartmentError.ApartmentNotFound);
            }

            DateRange duration = DateRange.Create(command.StartDate, command.EndDate);

            if (await _bookingRepository.IsOverlappingAsync(maybeApartment, duration))
            {
                return Result.Failure<Guid>(BookingErrors.Overlap);
            }

            var booking = Booking.Reserve(
                maybeApartment,
                command.UserId,
                duration, DateTime.UtcNow,
                _pricingService);

            _bookingRepository.Add(booking);

            await _unitofWork.SaveChangesAsync(cancellationToken);

            return booking.Id;
        }
    }
}
