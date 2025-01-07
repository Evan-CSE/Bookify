using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookify.Domain.Abstraction;
using Bookify.Domain.Review.Events;

namespace Bookify.Domain.Review
{
    public sealed class Review : Entity
    {
        public Guid ApartmentId { get; private set; }
        public Guid UserId { get; private set; }
        public Guid BookingId { get; private set; }
        public Rating Rating { get; private set; }
        public Comment Comment { get; private set; }

        private Review(
            Guid id,
            Guid apartmentId,
            Guid userId,
            Guid bookingId,
            Rating rating,
            Comment comment
        ) : base(id)
        {
            ApartmentId = apartmentId;
            UserId = userId;
            BookingId = bookingId;
            Rating = rating;
            Comment = comment;
        }

        public static Review Create(
            Guid apartmentId,
            Guid userId,
            Guid bookingId,
            Rating rating,
            Comment comment)
        {
            Review review = new(Guid.NewGuid(), apartmentId, userId, bookingId, rating, comment);

            review.RaiseDomainEvent(new ReviewCreatedDomainEvent(review.Id));
            return review;
        }
    }
}
