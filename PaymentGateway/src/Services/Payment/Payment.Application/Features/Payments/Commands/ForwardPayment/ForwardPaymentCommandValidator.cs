using FluentValidation;

namespace PaymentNs.Application.Features.Payments.Commands.ForwardPayment
{
    public class ForwardPaymentCommandValidator : AbstractValidator<ForwardPaymentCommand>
    {
        public ForwardPaymentCommandValidator()
        {
            RuleFor(p => p.CardNumber)
               .NotEmpty().WithMessage("{CardNumber} is required")
               .NotNull()
               .CreditCard().WithMessage("Invalid {CardNumber}");

            RuleFor(p => p.CVV)
               .NotEmpty().WithMessage("{CVV} is required")
               .NotNull()
               .Matches("^[0-9]{3,4}$").WithMessage("Invalid {CVV}");

            RuleFor(p => p.Currency)
               .NotEmpty().WithMessage("{Currency} is required")
               .NotNull();

            RuleFor(p => p.Amount)
               .NotEmpty().WithMessage("{Amount} is required")
               .NotNull();

            RuleFor(p => p.ExpiryMonth)
               .NotEmpty().WithMessage("{ExpiryMonth} is required")
               .NotNull()
               .InclusiveBetween(1, 12).WithMessage("Invalid {ExpiryMonth}");

            RuleFor(p => p.ExpiryYear.ToString())
               .NotEmpty().WithMessage("{ExpiryYear} is required")
               .NotNull()
               .Matches("^[0-9]{4}$").WithMessage("Invalid {ExpiryYear}");

            // Converting the expiry month and year to a valid date (taking 1st of the month) and comparing against today - 1 month
            // e.g. Expiry 12/2021 | Now 2021-12-31 --> 2021-12-01 greater than 2021-11-30 => valid
            //      Expiry 12/2021 | Now 2022-01-01 --> 2021-12-01 greater than 2021-12-01 => invalid
            RuleFor(p => DateTime.ParseExact(string.Format("{0}-{1}-01", p.ExpiryYear, p.ExpiryMonth), "yyyy-M-dd", null))
               .Must(BeValidDate).WithMessage("Card already expired");
        }

        protected bool BeValidDate(DateTime date)
        {
            return DateTime.Now.AddMonths(-1) < date;
        }
    }
}
