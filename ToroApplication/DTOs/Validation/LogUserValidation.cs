using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToroApplication.DTOs.Request;

namespace ToroApplication.DTOs.Validation
{
    public class LogUserValidation : AbstractValidator<LogUserDto>
    {
        public LogUserValidation()
        {
            RuleFor(c => c.CPF).NotEmpty();
            RuleFor(c => c.CPF).Length(11,14);
            RuleFor(c => c.Password).NotEmpty();
        }
    }
}
