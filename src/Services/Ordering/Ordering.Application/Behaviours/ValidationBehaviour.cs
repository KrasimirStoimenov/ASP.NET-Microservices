namespace Ordering.Application.Behaviours;

using System.Threading;
using System.Threading.Tasks;

using FluentValidation;

using MediatR;

using ValidationException = Exceptions.ValidatonException;

public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> validators;

    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
    {
        this.validators = validators ?? throw new ArgumentNullException(nameof(validators));
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        if (this.validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);

            var validationResults = await Task.WhenAll(this.validators.Select(x => x.ValidateAsync(context, cancellationToken)));
            var failures = validationResults.SelectMany(e => e.Errors).Where(x => x != null).ToList();
            if (failures.Count != 0)
            {
                throw new ValidationException(failures);
            }
        }

        return await next();
    }
}
