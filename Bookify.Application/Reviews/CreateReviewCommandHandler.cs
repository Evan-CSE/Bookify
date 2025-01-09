using Bookify.Application.Abstractions.Messaging;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Review;
using Bookify.Domain.Users;

namespace Bookify.Application.Reviews
{
    internal sealed class CreateReviewCommandHandler : ICommandHandler<CreateReviewCommand, Guid>
    {
        private readonly IUserRepository _userRepository;

        public Task<Result<Guid>> Handle(CreateReviewCommand command, CancellationToken cancellationToken)
        {
            Review review = Review.Create(command.ApartmentId, command.UserId, command.BookingId, command.Rating, command.Review);

            if (review != null)
            {
                return Task.FromResult(Result.Success(review.Id));
            }

            return Task.FromResult(Result.Failure<Guid>(ReviewError.NotEligible));
        }
    }
}
