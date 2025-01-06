using System.Diagnostics.CodeAnalysis;
using Bookify.Domain.Abstraction;

namespace Bookify.Domain.Abstraction
{
    public class Result
    {
        public bool IsSuccessful { get; private set; }
        public Error Error { get; private set; }
        internal Result(bool isSuccessful, Error error)
        {
            if (!isSuccessful && error != null)
            {
                throw new InvalidOperationException();
            }

            if (isSuccessful && error == null)
            {
                throw new InvalidOperationException();
            }

            IsSuccessful = isSuccessful;
            Error = error;
        }

        public static Result Success() => new Result(true, Error.None);

        public static Result Failure(Error error) => new Result(false, error);

        public static Result<TValue> Success<TValue>(TValue value) => new(value, true, Error.None);

        public static Result<TValue> Failure<TValue>(Error error) => new(default, false, error);
    }
}

public class Result<TValue> : Result
{
    public readonly TValue? _value;

    protected internal Result(TValue? value, bool isSuccessful, Error error) : base(isSuccessful, error)
    {
        _value = value;
    }

    [NotNull]
    public TValue Value => IsSuccessful
        ? _value 
        : throw new InvalidOperationException("Failed value cannot be read");
}
