using BankIntegration.Service.CQRSService.BankInquiryCQRSService.Query;
using FluentValidation;

namespace BankIntegration.Service.Validation.BankValidation;

public class GetShebaInquiryValidator : AbstractValidator<GetInquiryShebaQuery>
{
    public GetShebaInquiryValidator()
    {
        RuleFor(x => x.AccountNo)
            .NotEmpty().WithMessage("Account number must not be empty.")
            .NotNull().WithMessage("Account number must not be null.");

        RuleFor(x => x.AccountNo)
            .Length(26).WithMessage("Account number must be  26 characters long.");

        RuleFor(x => x.AccountNo)
            .NotEmpty().WithMessage("Account number must not be empty.")
            .Must(HasShebaStandard).WithMessage("Account number must start with 'IR' and be followed by 24 digits.");
    }

    private bool HasShebaStandard(string accountNo)
    {
        return !string.IsNullOrEmpty(accountNo) &&
               accountNo.StartsWith("IR") &&
               accountNo.Length == 26 &&
               accountNo.Substring(2).All(char.IsDigit);
    }
}