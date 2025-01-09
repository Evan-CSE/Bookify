using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Users
{
    public static class UserErrors
    {
        public static readonly Error UserNotFound = new("UserError.NotFound", "User not found");
        public static readonly Error UserNotCreated = new("UserError.UserNotCreated", "User could not be created");
    }
}
