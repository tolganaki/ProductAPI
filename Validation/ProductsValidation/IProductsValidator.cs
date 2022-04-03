using ProductAPI.Domain.Models;

namespace ProductAPI.Validation.ProductsValidation
{
    public interface IProductsValidator
    {
        string[] ValidateAddProduct(AddProductModel addProductModel);

        string[] ValidateUpdateProduct(ProductModel productModel);
    }
}