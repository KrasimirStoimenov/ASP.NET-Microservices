namespace Ordering.Application.Features.Orders.Commands.UpdateOrder;

using FluentValidation;

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(o => o.Username)
            .NotEmpty().WithMessage("Username is required.")
            .NotNull()
            .MaximumLength(50).WithMessage("Username must not exceed 50 characters");

        RuleFor(o => o.EmailAddress)
            .NotEmpty().WithMessage("Email Address is required.");

        RuleFor(o => o.TotalPrice)
            .NotEmpty().WithMessage("Total Price is required.")
            .GreaterThan(0).WithMessage("Total Price should be greated than zero");
    }
}
