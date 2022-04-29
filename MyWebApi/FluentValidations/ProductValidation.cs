using FluentValidation;
using MyWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi.FluentValidations
{
    public class ProductValidation : AbstractValidator<ProductDTO>
    {
        public ProductValidation()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100).WithMessage("Product Name Is Too Long");
            RuleFor(x => x.Price).NotEmpty();
            RuleFor(x => x.Context).MinimumLength(0).MaximumLength(500);
            RuleFor(x => x.CataLogId).NotEmpty();
        }
    }
}
