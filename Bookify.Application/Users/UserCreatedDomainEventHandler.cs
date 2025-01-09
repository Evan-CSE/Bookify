using Bookify.Application.Abstractions.Email;
using Bookify.Domain.Users;
using Bookify.Domain.Users.Events;
using MediatR;

namespace Bookify.Application.User
{
    internal sealed class UserCreatedDomainEventHandler : INotificationHandler<UserCreatedDomainEvent>
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;

        public async Task Handle(UserCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var maybeUser = await _userRepository.GetByIdAsync(notification.id);

            if (maybeUser == null)
            {
                return;
            }

            await _emailService.SendEmailAsync(maybeUser.Email, "User Registration Successful", "User created successfully");
        }
    }
}
