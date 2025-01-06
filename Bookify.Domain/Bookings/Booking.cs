using Bookify.Domain.Abstraction;
using Bookify.Domain.Apartments;
using Bookify.Domain.Bookings.Events;
using Bookify.Domain.Shared;

namespace Bookify.Domain.Bookings
{
    public sealed class Booking : Entity
    {
        private Booking(
            Guid id,
            Guid apartmentId,
            Guid userId,
            DateRange duration,
            Money priceForPeriod,
            Money cleaningFee,
            Money amenitiesFee,
            Money totalPrice,
            BookingStatus status,
            DateTime createdOnUtc,
            DateTime? confirmedOnUtc = null,
            DateTime? rejectedOnUtc = null,
            DateTime? compltedOnUtc = null,
            DateTime? cancelledOnUtc = null) : base(id)
        {
            ApartmentId = apartmentId;
            UserId = userId;
            Duration = duration;
            PriceForPeriod = priceForPeriod;
            CleaningFee = cleaningFee;
            AmenitiesUpCharge = amenitiesFee;
            TotalPrice = totalPrice;
            CreatedOnUtc = createdOnUtc;
            ConfirmedOnUtc = confirmedOnUtc;
            RejectedOnUtc = rejectedOnUtc;
            CompletedOnUtc = compltedOnUtc;
            CancelledOnutc = cancelledOnUtc;
        }

        public Guid ApartmentId { get; private set; }
        public Guid Userid { get; }
        public Guid UserId { get; private set; }
        public DateRange Duration { get; private set; }

        public Money PriceForPeriod { get; private set; }
        public Money CleaningFee { get; private set; }
        public Money AmenitiesUpCharge { get; private set; }
        public Money TotalPrice { get; private set; }
        public BookingStatus Status { get; private set; }
        public DateTime CreatedOnUtc { get; private set; }
        public DateTime? ConfirmedOnUtc { get; private set; }
        public DateTime? RejectedOnUtc { get; private set; }
        public DateTime? CompletedOnUtc { get; private set; }
        public DateTime? CancelledOnutc { get; private set; }

        public static Booking Reserve(
            Apartment apartment,
            Guid userId,
            DateRange duration,
            DateTime utcNow,
            PricingDetails pricingDetails
        )
        {
            Booking booking = new(
                Guid.NewGuid(),
                apartment.Id,
                userId,
                duration,
                pricingDetails.PriceForPeriod,
                pricingDetails.CleaningFee,
                pricingDetails.AmenitiesCharge,
                pricingDetails.TotalPrice,
                BookingStatus.Reserved,
                utcNow
            );

            booking.RaiseDomainEvent(new IBookingReservedEvent(booking.Id));

            apartment.LastBookedOnUtc = utcNow;

            return booking;
        }

        public Result Confirm (DateTime utcNow)
        {
            if (Status == BookingStatus.Reserved)
            {
                return Result.Failure(BookingErrors.NotPending);
            }

            Status = BookingStatus.Confirmed;

            ConfirmedOnUtc = utcNow;

            RaiseDomainEvent(new IBookingConfirmedEvent(utcNow));

            return Result.Success();
        }

        public Result Reject(DateTime utcNow)
        {
            if (Status == BookingStatus.Reserved)
            {
                return Result.Failure(BookingErrors.NotPending);
            }

            Status = BookingStatus.Rejected;

            RejectedOnUtc = utcNow;

            RaiseDomainEvent(new IBookingRejectedEvent(utcNow));

            return Result.Success();
        }
        public Result Complete(DateTime utcNow)
        {
            if (Status == BookingStatus.Reserved)
            {
                return Result.Failure(BookingErrors.NotPending);
            }

            Status = BookingStatus.Completed;

            CompletedOnUtc = utcNow;

            RaiseDomainEvent(new IBookingCompletedEvent(utcNow));

            return Result.Success();
        }

        public Result Cancel(DateTime utcNow)
        {
            if (Status != BookingStatus.Confirmed)
            {
                return Result.Failure(BookingErrors.NotConfirmed);
            }

            var currentDateUtc = DateOnly.FromDateTime(utcNow);

            if (currentDateUtc > Duration.Start)
            {
                return Result.Failure(BookingErrors.AlreadyStarted);
            }

            Status = BookingStatus.Cacelled;

            CancelledOnutc = utcNow;

            RaiseDomainEvent(new IBookingCancelledEvent(utcNow));

            return Result.Success();
        }
    }
}
