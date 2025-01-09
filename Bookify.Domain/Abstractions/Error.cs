namespace Bookify.Domain.Abstractions
{
    public record Error(string code, string name)
    {
        public static Error None = new(string.Empty, string.Empty);
    }
}
