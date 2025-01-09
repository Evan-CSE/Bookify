using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Apartments
{
    public static class ApartmentError
    {
        public static readonly Error ApartmentNotFound = new("ApartmentError.NotFound", "Apartment not found");
    }
}
