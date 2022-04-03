using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using ProductAPI.Controllers;
using ProductAPI.Domain.Enum;
using ProductAPI.Domain.Models;

namespace ProductAPI.Test
{
    [TestFixture]
    public class ProductsControllerTest : BaseControllerTest
    {
        private ProductsController _controller = null;

        [SetUp]
        public void Setup()
        {
            Configure();
            _controller = new ProductsController(_productService);
        }

        [Test, Order(1)]
        public async Task GetProducts_Test()
        {
            var response = await _controller.GetProducts();

            Assert.NotNull(response);
            Assert.IsTrue(response.Result is OkObjectResult);
            Assert.AreEqual(((response.Result as OkObjectResult).Value as IEnumerable<ProductModel>).Count(), 3);
        }

        [Test, Order(2)]
        public async Task GetProduct_Test()
        {
            var response = await _controller.GetProduct(1);

            Assert.NotNull(response);
            Assert.NotNull((response.Result as OkObjectResult).Value);
            Assert.AreEqual(((response.Result as OkObjectResult).Value as ProductModel).Name, "Product1");
        }

        [Test, Order(3)]
        public async Task GetProduct_ProductDoesNotExistTest()
        {
            var response = await _controller.GetProduct(4);

            Assert.NotNull(response);
            Assert.IsTrue(response.Result is NotFoundObjectResult);
        }

        [Test, Order(4)]
        public async Task AddProduct_Test()
        {
            var response = await _controller.AddProduct(new AddProductModel()
            {
                Name = "Product4",
                Category = "Category2",
                TypeId = ((byte)ProductType.ProductType3)
            });

            Assert.NotNull(response);
            Assert.IsTrue(response.Result is CreatedAtActionResult);
        }

        [Test, Order(5)]
        public async Task AddProduct_ProductWithSameNameExistsTest()
        {
            var response = await _controller.AddProduct(new AddProductModel()
            {
                Name = "Product1",
                Category = "Category1",
                TypeId = ((byte)ProductType.ProductType1)
            });

            Assert.NotNull(response);
            Assert.IsTrue(response.Result is ConflictObjectResult);
        }

        [Test, Order(6)]
        public async Task UpdateProduct_Test()
        {
            var response = await _controller.UpdateProduct(1, new ProductModel()
            {
                Id = 1,
                Name = "Product1",
                Category = "Category1",
                TypeId = ((byte)ProductType.ProductType2)
            });

            Assert.NotNull(response);
            Assert.IsTrue(response.Result is CreatedAtActionResult);
            Assert.AreEqual(((response.Result as CreatedAtActionResult).Value as ProductModel).TypeId, (byte)ProductType.ProductType2);
        }

        [Test, Order(7)]
        public async Task UpdateProduct_AnotherProductWithSameNameExistsTest()
        {
            var response = await _controller.UpdateProduct(1, new ProductModel()
            {
                Id = 1,
                Name = "Product2",
                Category = "Category1",
                TypeId = ((byte)ProductType.ProductType1)
            });

            Assert.NotNull(response);
            Assert.IsTrue(response.Result is ConflictObjectResult);
        }

        [Test, Order(8)]
        public async Task UpdateProduct_ProductDoesNotExistTest()
        {
            var response = await _controller.UpdateProduct(5, new ProductModel()
            {
                Id = 5,
                Name = "Product2",
                Category = "Category1",
                TypeId = ((byte)ProductType.ProductType1)
            });

            Assert.NotNull(response);
            Assert.IsTrue(response.Result is NotFoundObjectResult);
        }

        [Test, Order(9)]
        public async Task DeleteProduct_Test()
        {
            var response = await _controller.DeleteProduct(2);

            Assert.NotNull(response);
            Assert.NotNull(response is NoContentResult);
        }

        [Test, Order(10)]
        public async Task DeleteProduct_ProductDoesNotExistTest()
        {
            var response = await _controller.DeleteProduct(6);

            Assert.NotNull(response);
            Assert.NotNull(response is NotFoundObjectResult);
        }


        [Test, Order(11)]
        public async Task AddProduct_ValidationTest()
        {
            // Name is required
            var response = await _controller.AddProduct(new AddProductModel()
            {
                Name = "",
                Category = "Category2",
                TypeId = ((byte)ProductType.ProductType3)
            });

            Assert.NotNull(response);
            Assert.IsTrue(response.Result is BadRequestObjectResult);
            Assert.AreEqual(((response.Result as BadRequestObjectResult).Value as string[]), new string[] { "Name is required" });

            // Name is too long > 128 chars
            response = await _controller.AddProduct(new AddProductModel()
            {
                Name = "sdfsfg sdfsdfg sdfgsdfgsdfg  gfdsdfgsdfkgskldfgj sdfjgs jdfgjsdlkfgjsdkfjg sdfjgksdfjglksdfjglksdfjg klsdfjgskdfjgksdlfjgklsdfjgksldfjgslkdf",
                Category = "Category2",
                TypeId = ((byte)ProductType.ProductType3)
            });

            Assert.NotNull(response);
            Assert.IsTrue(response.Result is BadRequestObjectResult);
            Assert.AreEqual(((response.Result as BadRequestObjectResult).Value as string[]), new string[] { "Name can be maximum 128 characters long" });

            // Category is required
            response = await _controller.AddProduct(new AddProductModel()
            {
                Name = "Product56",
                Category = "",
                TypeId = ((byte)ProductType.ProductType1)
            });

            Assert.NotNull(response);
            Assert.IsTrue(response.Result is BadRequestObjectResult);
            Assert.AreEqual(((response.Result as BadRequestObjectResult).Value as string[]), new string[] { "Category is required" });

            // TypeId should be a valid value in ProductType
            response = await _controller.AddProduct(new AddProductModel()
            {
                Name = "Product56",
                Category = "Category2",
                TypeId = 5
            });

            Assert.NotNull(response);
            Assert.IsTrue(response.Result is BadRequestObjectResult);
        }

        [Test, Order(12)]
        public async Task UpdateProduct_ValidationTest()
        {
            // Name is required
            var response = await _controller.UpdateProduct(1, new ProductModel()
            {
                Id = 1,
                Name = "",
                Category = "Category1",
                TypeId = ((byte)ProductType.ProductType2)
            });

            Assert.NotNull(response);
            Assert.IsTrue(response.Result is BadRequestObjectResult);
            Assert.AreEqual(((response.Result as BadRequestObjectResult).Value as string[]), new string[] { "Name is required" });

            // Name is too long > 128 chars
            response = await _controller.UpdateProduct(1, new ProductModel()
            {
                Id = 1,
                Name = "sdfsfg sdfsdfg sdfgsdfgsdfg  gfdsdfgsdfkgskldfgj sdfjgs jdfgjsdlkfgjsdkfjg sdfjgksdfjglksdfjglksdfjg klsdfjgskdfjgksdlfjgklsdfjgksldfjgslkdf",
                Category = "Category1",
                TypeId = ((byte)ProductType.ProductType3)
            });

            Assert.NotNull(response);
            Assert.IsTrue(response.Result is BadRequestObjectResult);
            Assert.AreEqual(((response.Result as BadRequestObjectResult).Value as string[]), new string[] { "Name can be maximum 128 characters long" });

            // Category is required
            response = await _controller.UpdateProduct(1, new ProductModel()
            {
                Id = 1,
                Name = "Product1",
                Category = "",
                TypeId = ((byte)ProductType.ProductType1)
            });

            Assert.NotNull(response);
            Assert.IsTrue(response.Result is BadRequestObjectResult);
            Assert.AreEqual(((response.Result as BadRequestObjectResult).Value as string[]), new string[] { "Category is required" });

            // TypeId should be a valid value in ProductType
            response = await _controller.UpdateProduct(1, new ProductModel()
            {
                Id = 1,
                Name = "Product56",
                Category = "Category2",
                TypeId = 5
            });

            Assert.NotNull(response);
            Assert.IsTrue(response.Result is BadRequestObjectResult);
        }
    }
}