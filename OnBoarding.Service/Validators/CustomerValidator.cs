using FluentValidation;
using OnBoarding.Core.DTO;

namespace OnBoarding.Service.Validators
{
    public class CustomerValidator : AbstractValidator<CustomerDTO>
    {
        public CustomerValidator()
        {
            RuleFor(p => p.Email)
                .EmailAddress()
                .NotEmpty().WithMessage("{EmailAddress} is required.");

            RuleFor(p => p.PhoneNumber)
                .NotEmpty().WithMessage("{PhoneNumber} is required.")
                .NotNull()
                .MinimumLength(10).WithMessage("{PhoneNumber} must not less than 10 characters.")
                .MaximumLength(13).WithMessage("{PhoneNumber} must not exceed 13 characters.");


            RuleFor(p => p.Password).NotNull()
                .NotEmpty().WithMessage("{Password} is required.")
                .MinimumLength(7).WithMessage("{Password} length should not be less than 7 characters.");

            RuleFor(p => p.StateId)
                .NotNull()
                .NotEmpty().WithMessage("{StateId} is required.")
                .LessThanOrEqualTo(37).WithMessage("{State_Id} should be greater than zero.")
                .GreaterThan(0).WithMessage("{StateId} should be greater than zero.");

            RuleFor(p => p.LgaId)
                .NotNull().NotEmpty()
                .NotEmpty().WithMessage("{LgaId} is required.")
                .GreaterThan(0).WithMessage("{LgaId} should be greater than zero.");
        }
    }
}
