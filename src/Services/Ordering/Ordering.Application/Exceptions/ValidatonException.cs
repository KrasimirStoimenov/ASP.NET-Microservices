namespace Ordering.Application.Exceptions;

using FluentValidation.Results;

public class ValidatonException : ApplicationException
{
    public ValidatonException()
        : base($"One or more validation failures have occured.")
    {
        this.Errors = new Dictionary<string, string[]>();
    }

    public ValidatonException(IEnumerable<ValidationFailure> failures)
        : this()
    {
        this.Errors = failures
            .GroupBy(n => n.PropertyName, e => e.ErrorMessage)
            .ToDictionary(fg => fg.Key, fg => fg.ToArray());
    }

    public IDictionary<string, string[]> Errors { get; }
}
