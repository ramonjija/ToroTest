using FluentValidation;
using ToroApplication.DTOs.Request;

namespace ToroApplication.DTOs.Validation
{
    public class LogUserValidation : AbstractValidator<LogUserDto>
    {
        public LogUserValidation()
        {
            RuleFor(c => c.CPF).NotEmpty().WithMessage("CPF is required");
            RuleFor(c => c.CPF).Length(11,14).WithMessage("The CPF's format is invalid");
            RuleFor(c => c.Password).NotEmpty().WithMessage("Password is required");
        }
    }
}
