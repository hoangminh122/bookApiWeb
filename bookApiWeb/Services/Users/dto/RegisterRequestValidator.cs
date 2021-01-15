using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookApiWeb.Services.Users.dto
{
    public class RegisterRequestValidator :AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.Email).EmailAddress().WithMessage("Email is invalid");
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First Name is Required !");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last Name is Required !").MinimumLength(5).WithMessage("Min length 5 char");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is Required !").MinimumLength(6).WithMessage("Min length 6 char");


        }
    }
}
