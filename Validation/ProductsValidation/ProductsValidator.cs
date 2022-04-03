using ProductAPI.Domain.Models;

namespace ProductAPI.Validation.ProductsValidation
{
    public class ProductsValidator : IProductsValidator
    {
        public string[] ValidateAddProduct(AddProductModel addProductModel)
        {
            return new AddProductValidator().ValidateModel(addProductModel);
        }

        public string[] ValidateUpdateProduct(ProductModel productModel)
        {
            return new UpdateProductValidator().ValidateModel(productModel);
        }
    }
}