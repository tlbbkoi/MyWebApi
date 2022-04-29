using FluentValidation;
using MyWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi.FluentValidations
{
    public class UserValidation : AbstractValidator<UserDTO>
    {
        public UserValidation()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6).MaximumLength(15)
                                    .WithMessage("Your Password is limit 6 to 15 character");
        }
    }
}
