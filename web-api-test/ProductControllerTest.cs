using all_web_mongo.Controllers;
using all_web_mongo.Interface;
using all_web_mongo.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace web_api_test
{
    public class ProductControllerTest
    {
        private readonly ProductController _controller;
        private readonly IProductRepository _service;
        public ProductControllerTest()
        {
            _service = new DataSeed();
            _controller = new ProductController(_service);
        }

        #region getProductTest
        [Fact]
        public void Get_WhenCalled_ReturnsOkResult()
        {
            // Act
            var okResult = _controller.GetProduct();
            // Assert
            Assert.IsType<IActionResult>(okResult as IActionResult);
        }
        [Fact]
        public void Get_WhenCalled_ReturnsAllItems()
        {
            // Act
            var okResult = _controller.GetProduct() as IActionResult;
            // Assert
            var items = Assert.IsType<List<Products>>(okResult);
            Assert.Equal(3, items.Count);
        }
        #endregion

        #region getproducByIdTest
        [Fact]
        public void GetById_UnknownGuidPassed_ReturnsNotFoundResult()
        {
            // Act
            var notFoundResult = _controller.GetProduct(0);
            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult);
        }
        [Fact]
        public void GetById_ExistingGuidPassed_ReturnsOkResult()
        {
            // Act
            var okResult = _controller.GetProduct(1);
            // Assert
            Assert.IsType<OkObjectResult>(okResult as IActionResult);
        }
        [Fact]
        public void GetById_ExistingGuidPassed_ReturnsRightItem()
        {
            // Act
            var okResult = _controller.GetProduct(2) as IActionResult;
            // Assert
            Assert.IsType<Products>(okResult);
            Assert.Equal(2, (okResult as Products).ProductId);
        }
        #endregion

        #region saveMethodTest
        [Fact]
        public void Add_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            var nameMissingItem = new Products()
            {
               ProductName="test1",
               ProductDescirption= "desc2"
            };
            _controller.ModelState.AddModelError("ProductName", "Required");
            // Act
            var badResponse = _controller.SaveProduct(nameMissingItem);
            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }
        [Fact]
        public void Add_ValidObjectPassed_ReturnsCreatedResponse()
        {
            // Arrange
            Products testItem = new Products()
            {
                ProductName = "test1",
                ProductDescirption = "desc2"
            };
            // Act
            var createdResponse = _controller.SaveProduct(testItem);
            // Assert
            Assert.IsType<CreatedAtActionResult>(createdResponse);
        }
        [Fact]
        public void Add_ValidObjectPassed_ReturnedResponseHasCreatedItem()
        {
            // Arrange
            Products testItem = new Products()
            {
                ProductName = "test1",
                ProductDescirption = "desc2"
            };
            // Act
            var createdResponse = _controller.SaveProduct(testItem) as IActionResult;
            var item = createdResponse as Products;
            // Assert
            Assert.IsType<Products>(item);
            Assert.Equal("test1", item.ProductName);
        }
        #endregion

        #region deleteTest

        [Fact]
        public void Remove_NotExistingGuidPassed_ReturnsNotFoundResponse()
        {

            // Act
            var badResponse = _controller.DeleteProduct(0);
            // Assert
            Assert.IsType<NotFoundResult>(badResponse);
        }
        [Fact]
        public void Remove_ExistingGuidPassed_ReturnsNoContentResult()
        {
            // Act
            var noContentResponse = _controller.DeleteProduct(5);
            // Assert
            Assert.IsType<NoContentResult>(noContentResponse);
        }
        [Fact]
        public void Remove_ExistingGuidPassed_RemovesOneItem()
        {
            // Act
            var okResponse = _controller.DeleteProduct(3);
        }

        #endregion
    }
}
