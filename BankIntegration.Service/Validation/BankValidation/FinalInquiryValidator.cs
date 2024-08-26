using BankIntegration.Service.CQRSService.BankInquiryCQRSService.Query;
using FluentValidation;

namespace BankIntegration.Service.Validation.BankValidation;

public class FinalInquiryValidator : AbstractValidator<FinalInquiryQuery>
{
    public FinalInquiryValidator()
    {
        RuleFor(x => x.TransactionId)
            .NotEmpty().WithMessage("Account number must not be empty.")
            .NotNull().WithMessage("Account number must not be null.");
    }
   
}