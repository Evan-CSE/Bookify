using Bookify.Domain.Review.Events;
using MediatR;

namespace Bookify.Application.Reviews
{
    public sealed class ReviewCreatedDomainEventHandler : INotificationHandler<ReviewCreatedDomainEvent>
    {

        public async Task Handle(ReviewCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            return;
        }
    }
}
