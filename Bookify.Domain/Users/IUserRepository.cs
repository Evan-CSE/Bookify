namespace Bookify.Domain.Users
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid guid, CancellationToken cancellation = default);

        void Add(User user);
    }
}
