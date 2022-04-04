using AutoMapper;
using ProductAPI.Constants;
using ProductAPI.Data;
using ProductAPI.Data.Entities;
using ProductAPI.Data.Repositories;
using ProductAPI.Domain.Models;
using ProductAPI.Domain.Response;

namespace ProductAPI.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<ProductModel>>> GetProducts()
        {
            var products = await _productRepository.ListAsync();
            var productModels = _mapper.Map<IEnumerable<ProductModel>>(products);

            return ApiResponseFactory.CreateSuccess(productModels);
        }

        public async Task<ApiResponse<ProductModel>> GetProduct(int id)
        {
            var product = await _productRepository.FindByIdAsync(id);

            if (product == null)
                return ApiResponseFactory.CreateError<ProductModel>(MessageConstants.PRODUCT_NOT_FOUND);

            var productModel = _mapper.Map<ProductModel>(product);
            return ApiResponseFactory.CreateSuccess(productModel);
        }

        public async Task<ApiResponse<ProductModel>> AddProduct(AddProductModel addProductModel)
        {
            var product = _mapper.Map<Product>(addProductModel);

            var productFound = await _productRepository.FindByNameAsync(addProductModel.Name);
            if (productFound != null)
            {
                return ApiResponseFactory.CreateError<ProductModel>(MessageConstants.ANOTHER_PRODUCT_WITH_SAME_NAME_EXISTS);
            }

            await _productRepository.AddAsync(product);
            await _unitOfWork.CommitAsync();

            var productModel = _mapper.Map<ProductModel>(product);
            return ApiResponseFactory.CreateSuccess(productModel);
        }

        public async Task<ApiResponse<ProductModel>> UpdateProduct(ProductModel productModel)
        {
            var product = _mapper.Map<Product>(productModel);

            var productFound = await _productRepository.FindByIdAsync(product.Id);
            if (productFound == null)
            {
                return ApiResponseFactory.CreateError<ProductModel>(MessageConstants.PRODUCT_NOT_FOUND);
            }

            var productToCheck = await _productRepository.FindByNameAsync(productModel.Name);
            if (productToCheck != null && productToCheck.Id != productFound.Id)
            {
                return ApiResponseFactory.CreateError<ProductModel>(MessageConstants.ANOTHER_PRODUCT_WITH_SAME_NAME_EXISTS, nameof(MessageConstants.ANOTHER_PRODUCT_WITH_SAME_NAME_EXISTS));
            }

            productFound.Name = product.Name;
            productFound.TypeId = product.TypeId;
            productFound.Category = product.Category;

            _productRepository.Update(productFound);
            await _unitOfWork.CommitAsync();

            productModel = _mapper.Map<ProductModel>(productFound);
            return ApiResponseFactory.CreateSuccess(productModel);
        }

        public async Task<ApiResponse> DeleteProduct(int id)
        {
            var product = await _productRepository.FindByIdAsync(id);
            if (product == null)
            {
                return ApiResponseFactory.CreateError(MessageConstants.PRODUCT_NOT_FOUND);
            }

            _productRepository.Delete(product);
            await _unitOfWork.CommitAsync();

            return ApiResponseFactory.CreateSuccess();
        }
    }
}