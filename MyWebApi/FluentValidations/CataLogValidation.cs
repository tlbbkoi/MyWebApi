using FluentValidation;
using MyWebApi.Models;
using MyWebApi.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi.FluentValidations
{
    public class CataLogValidation : AbstractValidator<CataLogDTO>
    {
        public CataLogValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(Resource.DATA_IS_REQUIRED).MaximumLength(100).WithMessage(string.Format(Resource.VALIDATION_NOT_EMPTY,"Tên"));
        }
    }
}
