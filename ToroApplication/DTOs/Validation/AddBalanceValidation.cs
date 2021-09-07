using FluentValidation;
using ToroApplication.DTOs.Request;

namespace ToroApplication.DTOs.Validation
{
    public class AddBalanceValidation : AbstractValidator<AddBalanceDto>
    {
        public AddBalanceValidation()
        {
            RuleFor(c => c.Balance).NotEmpty().WithMessage("Balance is required");
        }
    }
}
