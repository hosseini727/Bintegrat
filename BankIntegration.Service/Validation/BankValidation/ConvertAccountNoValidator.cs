using BankIntegration.Service.CQRSService.BankInquiryCQRSService.Query;
using FluentValidation;

namespace BankIntegration.Service.Validation.BankValidation;

public class ConvertAccountNoValidator : AbstractValidator<ConvertAccountNoQuery>
{
    public ConvertAccountNoValidator()
    {
        RuleFor(x => x.DepositNo)
            .NotEmpty().WithMessage("Account number must not be empty.")
            .NotNull().WithMessage("Account number must not be null.");
    }
   
}