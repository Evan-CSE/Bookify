using Bookify.Domain.Abstraction;

namespace Bookify.Domain.Users.Events
{
    public record UserCreatedDomainEvent (Guid id) : IDomainEvent
    {
    }
}
