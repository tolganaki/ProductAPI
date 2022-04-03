using FluentValidation;
using ProductAPI.Domain.Enum;
using ProductAPI.Domain.Models;

namespace ProductAPI.Validation.ProductsValidation
{
    public class AddProductValidator : BaseValidator<AddProductModel>
    {
        public AddProductValidator()
        {
            RuleFor(q => q.Name).NotEmpty().WithMessage(NOT_EMPTY_ERROR_MESSAGE);
            RuleFor(q => q.Name).MaximumLength(128).WithMessage(MAX_LENGTH_ERROR_MESSAGE);

            RuleFor(q => q.Category).NotEmpty().WithMessage(NOT_EMPTY_ERROR_MESSAGE);
            RuleFor(q => q.Category).MaximumLength(128).WithMessage(MAX_LENGTH_ERROR_MESSAGE);

            RuleFor(q => q.TypeId).Must(q => Enum.IsDefined(typeof(ProductType), q)).WithMessage(NOT_IN_ENUM_ERROR_MESSAGE);
        }
    }
}