using Bookify.Domain.Abstraction;

namespace Bookify.Domain.Review
{
    public record Rating
    {
        public static readonly Error InvalidRating = new("Rating.Invalid", "Rating should be in range (0-5)");
        private int Value { get; init; }

        protected Rating(int Value)
        {
            this.Value = Value;
        }

        public static Result<Rating> Create(int value)
        {
            if (value < 1 || value > 5)
            {
                return Result.Failure<Rating>(InvalidRating);
            }
            return Result.Success(new Rating(value));
        }
    }
}