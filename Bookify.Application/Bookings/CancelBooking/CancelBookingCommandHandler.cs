using Bookify.Application.Abstractions.Messaging;
using Bookify.Domain.Abstraction;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Bookings;
using Bookify.Domain.Users;

namespace Bookify.Application.Bookings.CancelBooking
{
    public sealed class CancelBookingCommandHandler : ICommandHandler<CancelBookingCommand, bool>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitofWork _unitOfWork;
        public async Task<Result<bool>> Handle(CancelBookingCommand request, CancellationToken cancellationToken)
        {
            var maybeBookingInstance = await _bookingRepository.GetByIdAsync(request.bookingId);

            if (maybeBookingInstance == null)
            {
                return Result.Failure<bool>(BookingErrors.NotFound);
            }

            var maybeUser = await _userRepository.GetByIdAsync(request.userId);

            if (maybeUser == null)
            {
                return Result.Failure<bool>(UserErrors.UserNotFound);
            }

            // also check if user is not admin
            // TODO: Currently, we don't have user role associated. But do this if we introduce user roles.
            if (maybeBookingInstance.UserId != maybeUser.Id)
            {
                return Result.Failure<bool>(BookingErrors.NotAuthorized);
            }

            maybeBookingInstance.Cancel(DateTime.UtcNow);

            await _unitOfWork.SaveChangesAsync();

            return Result.Success(true);
        }
    }
}
