using Bookify.Application.Abstractions.Messaging;
using Bookify.Application.Exceptions;
using FluentValidation;
using MediatR;

namespace Bookify.Application.Abstractions.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICommand
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!_validators.Any())
            {
                return await next();
            }

            var validationContext = new ValidationContext<TRequest>(request);

            var validationErrors = _validators
                .Select(validator => validator.Validate(validationContext))
                .Where(validationResult => validationResult.Errors.Any())
                .SelectMany(validationErrors => validationErrors.Errors)
                .Select(validationFailure => new ValidationError
                (
                    validationFailure.PropertyName,
                    validationFailure.ErrorMessage
                ))
                .ToList();

            if (validationErrors.Any())
            {
                throw new Exceptions.ValidationException(validationErrors);
            }
            return await next();
        }
    }
}
