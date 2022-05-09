using FluentValidation;
using MyWebApi.Models;
using MyWebApi.Properties;
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
            RuleFor(x => x.Email).NotEmpty().WithMessage(string.Format(Resource.VALIDATION_NOT_EMPTY, "Email"))
                .EmailAddress().WithMessage(string.Format(Resource.VALIDATION_DISPLAY, "Email"));
            RuleFor(x => x.Password).NotEmpty().WithMessage(string.Format(Resource.VALIDATION_NOT_EMPTY, "Password"))
                .MinimumLength(8).WithMessage(string.Format(Resource.MIN_LENGTH,8))
                ;
        }
    }

    public class ResetPassWordValidation : AbstractValidator<ResetPassword>
    {
        public ResetPassWordValidation()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage(string.Format(Resource.VALIDATION_NOT_EMPTY, "Email"))
               .EmailAddress().WithMessage(string.Format(Resource.VALIDATION_DISPLAY, "Email"));
            RuleFor(x => x.Password).NotEmpty().WithMessage(string.Format(Resource.VALIDATION_NOT_EMPTY, "Password"))
                .MaximumLength(200).WithMessage(string.Format(Resource.VALIDATION_MAX_LENGTH, "Password", "200"));
            RuleFor(x => x).Custom((request, context) =>
            {
                if (request.Password != request.ConfirmPassword)
                {
                    context.AddFailure(string.Format(Resource.VALIDATION_COMPARE, "Password"));
                }
            });
        }
    }
}
