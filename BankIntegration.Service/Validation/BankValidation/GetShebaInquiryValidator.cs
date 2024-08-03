using BankIntegration.Service.CQRSService.BankInquiryCQRSService.Query;
using FluentValidation;

namespace BankIntegration.Service.Validation.BankValidation;

public class GetShebaInquiryValidator : AbstractValidator<GetInquiryShebaQuery>
{
    public GetShebaInquiryValidator()
    {
        RuleFor(x => x.AccountNo).NotEmpty().WithMessage("test");
    }
}