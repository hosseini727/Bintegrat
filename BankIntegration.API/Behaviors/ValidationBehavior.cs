using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace BankIntegration.API.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    private readonly List<ValidationFailure> _exceptions;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators, List<ValidationFailure> exceptions)
    {
        _validators = validators;
        _exceptions = new List<ValidationFailure>();
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any())
            return await next();
        var context = new ValidationContext<TRequest>(request);
        var validationFailures = _validators
            .Select(validator => validator.Validate(request))
            .SelectMany(validationResult => validationResult.Errors)
            .Where(validationFailure => validationFailure != null)
            .ToList();
        if (validationFailures.Any())
        {
            foreach (var ex in validationFailures)
            {
                _exceptions.Add(ex);
            }

            var error = string.Join("\r\n", validationFailures);
            throw new ValidationException(error, _exceptions);
        }

        return await next();
    }
}