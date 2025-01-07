namespace Bookify.Domain.Apartments
{
    public interface IApartmentRepository
    {
        Task<Apartment?> GetByIdAsync(Guid guid, CancellationToken cancellationToken = default); 
    }
}
