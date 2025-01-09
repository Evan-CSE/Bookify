using Bookify.Application.Abstractions.Messaging;
using Bookify.Domain.Users;

namespace Bookify.Application.User
{
    public record class CreateUserCommand(
        FirstName FirstName,
        LastName LastName,
        Email Email
    ) : ICommand<Guid>;
}
