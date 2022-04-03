using FluentValidation;

namespace ProductAPI.Validation
{
    public abstract class BaseValidator<T> : AbstractValidator<T>
    {
        protected string NOT_EMPTY_ERROR_MESSAGE = "{PropertyName} is required";
        protected string MAX_LENGTH_ERROR_MESSAGE = "{PropertyName} can be maximum {MaxLength} characters long";
        protected string NOT_IN_ENUM_ERROR_MESSAGE = "{PropertyName} is not a valid value";

        public string[] ValidateModel(T model)
        {
            var result = Validate(model);
            return result.Errors.Select(x => x.ErrorMessage).ToArray();
        }
    }
}