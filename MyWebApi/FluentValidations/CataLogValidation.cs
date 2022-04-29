using FluentValidation;
using MyWebApi.Models;
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
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100).WithMessage("CaTalog Product Is Too Log");
        }
    }
}
