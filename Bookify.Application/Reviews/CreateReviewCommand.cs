using Bookify.Application.Abstractions.Messaging;
using Bookify.Domain.Review;

namespace Bookify.Application.Reviews
{
    internal sealed record CreateReviewCommand(Comment Review, Guid UserId, Guid BookingId, Guid ApartmentId, Rating Rating) : ICommand<Guid>;
}
