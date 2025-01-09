namespace Bookify.Domain.Abstraction
{
    public interface IUnitofWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
