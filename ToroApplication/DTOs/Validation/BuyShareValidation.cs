using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToroApplication.DTOs.Request;

namespace ToroApplication.DTOs.Validation
{
    public class BuyShareValidation : AbstractValidator<BuyShareDto>
    {
        public BuyShareValidation()
        {
            RuleFor(c => c.ShareSymbol).NotEmpty();
            RuleFor(c => c.Amout).NotEmpty();
        }
    }
}
