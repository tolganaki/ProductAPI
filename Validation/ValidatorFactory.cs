using ProductAPI.Validation.ProductsValidation;

namespace ProductAPI.Validation
{
    public static class ValidatorFactory
    {
        public static IProductsValidator GetProductsValidator()
        {
            return new ProductsValidator();
        }
    }
}