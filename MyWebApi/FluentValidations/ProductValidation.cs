using FluentValidation;
using MyWebApi.Models;
using MyWebApi.Properties;
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
            RuleFor(x => x.Name).NotEmpty().WithMessage(string.Format(Resource.VALIDATION_NOT_EMPTY))
                .MaximumLength(100).WithMessage(string.Format(Resource.VALIDATION_MAX_LENGTH,"Tên sản phẩm"));
            RuleFor(x => x.Price).NotEmpty().WithMessage(string.Format(Resource.VALIDATION_NOT_EMPTY,"Giá sản phẩm"));
            RuleFor(x => x.Context).MinimumLength(0).MaximumLength(500).WithMessage(string.Format(Resource.VALIDATION_MAX_LENGTH,"Thông tin sản phẩm","500"));
            RuleFor(x => x.CataLogId).NotEmpty().WithMessage(string.Format(Resource.VALIDATION_NOT_EMPTY));
        }
    }
}
