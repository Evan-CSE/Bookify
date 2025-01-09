namespace Bookify.Domain.Users
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid guid, CancellationToken cancellation = default);

        Task<User> Add(User user);
    }
}
