using FluentValidation;
using ToroApplication.DTOs.Request;

namespace ToroApplication.DTOs.Validation
{
    public class BuyShareValidation : AbstractValidator<BuyShareDto>
    {
        public BuyShareValidation()
        {
            RuleFor(c => c.ShareSymbol).NotEmpty().WithMessage("Share is required");
            RuleFor(c => c.Amount).NotEmpty().WithMessage("Amount is required");
        }
    }
}
