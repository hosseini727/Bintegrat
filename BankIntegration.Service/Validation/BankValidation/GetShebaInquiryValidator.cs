using BankIntegration.Service.CQRSService.BankInquiryCQRSService.Query;
using FluentValidation;
using System.Text.RegularExpressions;

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
            .Must(HasShebaStandard).WithMessage("Account number must start with 'IR' and be followed by 24 digits.")
            .Must(CheckSheba).WithMessage("Sheba entered is wrong");
    }

    private bool CheckSheba(string accountNo)
    {
        var isSheba = Regex.IsMatch(accountNo, "^[a-zA-Z]{2}\\d{2} ?\\d{4} ?\\d{4} ?\\d{4} ?\\d{4} ?[\\d]{0,2}",
               RegexOptions.Compiled);

        if (!isSheba)
        {
            return false;
        }
        else
        {           
            accountNo = accountNo.ToLower();
            var get4FirstDigit = accountNo.Substring(0, 4);
            var replacedGet4FirstDigit = get4FirstDigit.ToLower().Replace("i", "18").Replace("r", "27");
            var removedShebaFirst4Digit = accountNo.Replace(get4FirstDigit, "");
            var newSheba = removedShebaFirst4Digit + replacedGet4FirstDigit;
            var finalLongData = Convert.ToDecimal(newSheba);
            var finalReminder = finalLongData % 97;
            if (finalReminder == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }            
    }


    private bool HasShebaStandard(string accountNo)
    {
        return !string.IsNullOrEmpty(accountNo) &&
               accountNo.StartsWith("IR") &&
               accountNo.Length == 26 &&
               accountNo.Substring(2).All(char.IsDigit);
    }
}