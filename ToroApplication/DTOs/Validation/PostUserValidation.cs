using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToroApplication.DTOs.Request;

namespace ToroApplication.DTOs.Validation
{
    public class PostUserValidation : AbstractValidator<PostUserDto>
    {
        public PostUserValidation()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(c => c.CPF).NotEmpty().WithMessage("CPF is required"); ;
            RuleFor(c => c.CPF).Length(11,14).WithMessage("The CPF's format is invalid"); ;
            RuleFor(c => c.Password).NotEmpty().WithMessage("Password is required");
        }
    }
}
