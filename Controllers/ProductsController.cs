using Microsoft.AspNetCore.Mvc;
using ProductAPI.Constants;
using ProductAPI.Domain.Controllers;
using ProductAPI.Domain.Models;
using ProductAPI.Services.Products;
using ProductAPI.Validation;

namespace ProductAPI.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Returns all the products.
        /// url= .../api/products
        /// </summary>
        /// <returns>List of Products</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductModel>>> GetProducts()
        {
            var response = await _productService.GetProducts();
            return Ok(response.Data);
        }

        /// <summary>
        /// Returns the Product with the given Id
        /// url= .../api/products/1
        /// </summary>
        /// <param name="id">id of the Product</param>
        /// <returns>Product</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductModel>> GetProduct(int id)
        {
            var response = await _productService.GetProduct(id);
            if (response.Success)
                return Ok(response.Data);
            else
                return NotFound(response.Message);
        }

        /// <summary>
        /// Adds a Product
        /// url= .../api/products
        /// </summary>
        /// <param name="addProductModel">The model which contains the new Product properties</param>
        /// <returns>Newly added Product</returns>
        [HttpPost]
        public async Task<ActionResult<ProductModel>> AddProduct(AddProductModel addProductModel)
        {
            var validationErrors = ValidatorFactory.GetProductsValidator().ValidateAddProduct(addProductModel);
            if (validationErrors.Length > 0)
                return BadRequest(validationErrors);

            addProductModel.Name = addProductModel.Name.Trim();
            addProductModel.Category = addProductModel.Category.Trim();

            var response = await _productService.AddProduct(addProductModel);
            if (response.Success)
                return CreatedAtAction(nameof(AddProduct), response.Data);
            else
                return Conflict(response.Message);
        }

        /// <summary>
        /// Updates the Product
        /// url= .../api/products/3
        /// </summary>
        /// <param name="id">id of the Product</param>
        /// <param name="product">The model which contains Product properties to update</param>
        /// <returns>Updated Product</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<ProductModel>> UpdateProduct(int id, ProductModel product)
        {
            var validationErrors = ValidatorFactory.GetProductsValidator().ValidateUpdateProduct(product);
            if (validationErrors.Length > 0)
                return BadRequest(validationErrors);

            product.Id = id;
            product.Name = product.Name.Trim();
            product.Category = product.Category.Trim();

            var response = await _productService.UpdateProduct(product);

            if (response.Success)
                return CreatedAtAction(nameof(UpdateProduct), response.Data);
            else
            {
                if (response.Tag == nameof(MessageConstants.ANOTHER_PRODUCT_WITH_SAME_NAME_EXISTS))
                    return Conflict(response.Message);
                else
                    return NotFound(response.Message);
            }
        }

        /// <summary>
        /// Deletes the Product
        /// url= .../api/products/2
        /// </summary>
        /// <param name="id">id of the Product</param>
        /// <returns>Http Status Code of 204 or 404</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var response = await _productService.DeleteProduct(id);

            if (response.Success)
                return NoContent();
            else
                return NotFound(response.Message);
        }
    }
}