using Bookify.Application.Abstractions.Messaging;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Users;

namespace Bookify.Application.User
{
    public sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var userInstance = Bookify.Domain.Users.User.Create(request.FirstName, request.LastName, request.Email);

            if (userInstance != null)
            {
                return Result.Failure<Guid>(UserErrors.UserNotCreated);
            }

           var user = await _userRepository.Add(userInstance);

            return Result.Success<Guid>(user.Id);
        }
    }
}
