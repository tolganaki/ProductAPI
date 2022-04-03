using ProductAPI.Domain.Models;
using ProductAPI.Domain.Response;

namespace ProductAPI.Services.Products
{
    public interface IProductService
    {
        Task<ApiResponse<IEnumerable<ProductModel>>> GetProducts();

        Task<ApiResponse<ProductModel>> GetProduct(int id);

        Task<ApiResponse<ProductModel>> AddProduct(AddProductModel addProductModel);

        Task<ApiResponse<ProductModel>> UpdateProduct(ProductModel productModel);

        Task<ApiResponse> DeleteProduct(int id);
    }
}